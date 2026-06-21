using UnityEngine;
using UnityEngine.UI; // <- если используешь обычный Text
using System.Collections;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.1f; // радиус проверки
    public LayerMask whatIsGround;   // слой платформ

    [Header("Audio")]
    public AudioSource jumpSound;
    public AudioClip jumpClip;
    private float lastJumpTime = -1f;
    private float jumpCooldown = 0.25f; // короткий интервал между звуками

    public bool jumpBoostActive = false;
    public float jumpMultiplier = 1f; // обычный множитель

    public float baseSpeed;
    public float speed;
    public float forceJump;
    float moveInput;
    Rigidbody2D rb;

    [Header("UI")]
    public TMP_Text jumpBoostTimerText; // ссылка на UI Text для таймера

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        StatsUpdate();

        // 🔹 если буст прыжка куплен в меню — активируем на 30 секунд
        if (BoostManager.Instance != null && BoostManager.Instance.jumpBoost)
        {
            ActivateJumpBoost(30f, 2f); // 30 секунд, x2 прыжок
            BoostManager.Instance.jumpBoost = false; // сбрасываем, чтобы был на один забег
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (rb.velocity.x < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (rb.velocity.x > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void MoveLeft() { moveInput = -1f; }
    public void MoveRight() { moveInput = 1f; }
    public void StopMove() { moveInput = 0f; }

    public void StatsUpdate()
    {
        speed = PlayerPrefs.GetInt("Player Speed");
        speed = baseSpeed + speed * 2f;
    }

    public void ActivateJumpBoost(float duration, float multiplier)
    {
        StartCoroutine(JumpBoostRoutine(duration, multiplier));
    }

    private IEnumerator JumpBoostRoutine(float duration, float multiplier)
    {
        jumpBoostActive = true;
        jumpMultiplier = multiplier;

        // 🔹 показываем таймер на UI
        if (jumpBoostTimerText != null)
            jumpBoostTimerText.gameObject.SetActive(true);

        float timeLeft = duration;
        while (timeLeft > 0)
        {
            if (jumpBoostTimerText != null)
                jumpBoostTimerText.text = "" + Mathf.Ceil(timeLeft) + "";

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        jumpBoostActive = false;
        jumpMultiplier = 1f;

        // 🔹 скрываем таймер
        if (jumpBoostTimerText != null)
            jumpBoostTimerText.gameObject.SetActive(false);
    }
    bool IsGrounded()
{
    return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
}

    public void Jump()
{
    // проверка, чтобы прыгать только если игрок "на земле"
    if (IsGrounded()) // сюда нужно вставить твою проверку касания земли
    {
        rb.velocity = new Vector2(rb.velocity.x, forceJump * jumpMultiplier);

        // проигрываем звук с cooldown
        if(Time.time - lastJumpTime > jumpCooldown)
        {
            jumpSound.PlayOneShot(jumpClip);
            lastJumpTime = Time.time;
        }
    }
}

}
