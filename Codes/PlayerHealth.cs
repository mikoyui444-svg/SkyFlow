using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("Health")]
    public int maxHearts = 3;
    public float maxHealth;
    public float currentHealth;

    [Header("State")]
    public bool isInvulnerable = false; // 🛡 НЕУЯЗВИМОСТЬ (RocketBall)

    [Header("UI")]
    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    [Header("Smooth")]
    public float smoothSpeed = 5f;

    [Header("Sounds")]
    public AudioClip hurtSound;
    public AudioClip healSound;
    private AudioSource audioSource;

    private bool soundPlayed = false;

    // =========================
    // Чекпоинт
    // =========================
    private Vector3 respawnPos;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        maxHealth = maxHearts * 100f;
        currentHealth = maxHealth;
        UpdateHeartsInstant();

        audioSource = GetComponent<AudioSource>();

        // стартовая точка возрождения
        respawnPos = transform.position;
    }

    // =========================
    // Чекпоинт
    // =========================
    public void UpdateRespawnPoint(Vector3 newPos)
    {
        respawnPos = newPos;
    }

    // =========================
    // УРОН / ЛЕЧЕНИЕ
    // =========================
    public void TakeDamage(float damage)
    {
        // 🛡 НЕУЯЗВИМОСТЬ (RocketBall)
        if (isInvulnerable && damage > 0)
            return;

        // 🛡 ЩИТ
        PlayerShield shield = GetComponent<PlayerShield>();
        if (shield != null && shield.isShieldActive && damage > 0)
            return;

        float targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (!soundPlayed)
        {
            if (damage > 0 && hurtSound != null)
                audioSource.PlayOneShot(hurtSound);
            else if (damage < 0 && healSound != null)
                audioSource.PlayOneShot(healSound);

            soundPlayed = true;
        }

        StopAllCoroutines();
        StartCoroutine(SmoothHealthChange(targetHealth));
    }

    IEnumerator SmoothHealthChange(float target)
    {
        while (Mathf.Abs(currentHealth - target) > 10f)
        {
            currentHealth = Mathf.Lerp(currentHealth, target, Time.deltaTime * smoothSpeed);
            UpdateHearts();
            yield return null;
        }

        currentHealth = target;
        UpdateHearts();
        soundPlayed = false;

        if (currentHealth <= 0.01f)
        {
            currentHealth = 0;
            Die();
        }
    }

    // =========================
    // UI СЕРДЕЦ
    // =========================
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            float heartValue = Mathf.Clamp(currentHealth - (i * 100f), 0, 100f);

            if (heartValue >= 75f)
                hearts[i].sprite = heartFull;
            else if (heartValue >= 25f)
                hearts[i].sprite = heartHalf;
            else
                hearts[i].sprite = heartEmpty;
        }
    }

    void UpdateHeartsInstant()
    {
        UpdateHearts();
    }

    // =========================
    // СМЕРТЬ
    // =========================
    void Die()
    {
        // 🔥 сброс всех бустов
        if (BoostManager.Instance != null)
            BoostManager.Instance.ResetBoosts();

        isInvulnerable = false; // ⛔ на всякий случай

        GameManager.Instance.GameOver();
    }

    // =========================
    // REVIVE / CONTINUE
    // =========================
    public void Revive()
    {
        StopAllCoroutines();

        transform.position = respawnPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        currentHealth = maxHealth;
        isInvulnerable = false; // ⛔ сброс неуязвимости
        UpdateHeartsInstant();

        gameObject.SetActive(true);
    }
}
