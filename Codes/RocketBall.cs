using UnityEngine;

public class RocketBall : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float rocketForce = 15f;   // сила подъёма
    public float flightTime = 2f;     // время полёта

    [Header("Effects & Audio")]
    public GameObject explosionEffect;
    public AudioSource audioSource;
    public AudioClip rocketClip;

    private bool isHeld = false;
    private Transform player;
    private Transform handPosition;
    private float timer = 0f;

    private Rigidbody2D playerRb;
    private PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isHeld) return;

        if (col.CompareTag("Player"))
        {
            player = col.transform;
            isHeld = true;

            // Rigidbody игрока
            playerRb = player.GetComponent<Rigidbody2D>();

            // Health игрока
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.isInvulnerable = true; // 🛡 включаем неуязвимость

            // Точка в руках
            handPosition = player.Find("HandPoint");
            if (handPosition == null)
                handPosition = player;

            // Прикрепляем шарик
            transform.position = handPosition.position;
            transform.parent = handPosition;

            // Звук
            if (audioSource != null && rocketClip != null)
                audioSource.PlayOneShot(rocketClip);
        }
    }

    private void Update()
    {
        if (!isHeld || playerRb == null) return;

        // Поднимаем игрока вверх
        playerRb.velocity = new Vector2(playerRb.velocity.x, rocketForce);

        // Таймер
        timer += Time.deltaTime;
        if (timer >= flightTime)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Выключаем неуязвимость
        if (playerHealth != null)
            playerHealth.isInvulnerable = false;

        // Отцепляем от игрока
        transform.parent = null;

        // Эффект
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
