using UnityEngine;

public class Platform : MonoBehaviour
{
    public float forceJump = 10f;
    private float lastJumpSoundTime = -1f;
    private float jumpSoundCooldown = 0.25f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rb = collision.rigidbody;
        if (rb == null) return;

        // Игрок падает сверху
        if (collision.relativeVelocity.y >= 0) return;

        float finalJump = forceJump;

        // Проверяем буст прыжка
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null && player.jumpBoostActive)
            finalJump *= player.jumpMultiplier;

        rb.velocity = Vector2.up * finalJump;

        // Звук прыжка с cooldown
        if (player != null && player.jumpSound != null && player.jumpClip != null)
        {
            if (Time.time - lastJumpSoundTime > jumpSoundCooldown)
            {
                player.jumpSound.PlayOneShot(player.jumpClip);
                lastJumpSoundTime = Time.time;
            }
        }

        // Respawn платформы при попадании в DeathZone
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            float randX = Random.Range(-5f, 5f);
            float randY = transform.position.y + 16f;
            transform.position = new Vector3(randX, randY, transform.position.z);
        }
    }
}
