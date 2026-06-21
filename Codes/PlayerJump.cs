using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public AudioSource jumpSound;
    public AudioClip jumpClip;

    private float lastJumpTime = -1f;
    private float jumpCooldown = 0.25f;

    void Update()
    {
        // проверка на нажатие кнопки прыжка
        if(Input.GetButtonDown("Jump")) // или твоя кнопка UI
        {
            Jump();
        }
    }

    void Jump()
    {
        // прыжок
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // проигрываем звук с cooldown
        if(Time.time - lastJumpTime > jumpCooldown)
        {
            jumpSound.PlayOneShot(jumpClip);
            lastJumpTime = Time.time;
        }
    }
}
