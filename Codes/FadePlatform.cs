using UnityEngine;

public class FadePlatform : MonoBehaviour
{
    [Header("Fade Settings")]
    public float disappearTime = 1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip stepClip;

    private bool triggered = false; // чтобы не срабатывало несколько раз

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (triggered)
            return;

        if (!col.collider.CompareTag("Player"))
            return;

        Rigidbody2D playerRb = col.collider.GetComponent<Rigidbody2D>();
        if (playerRb == null)
            return;

        // 🔹 игрок должен падать вниз
        if (playerRb.velocity.y <= 0f)
        {
            // 🔹 проверяем, что игрок ВЫШЕ платформы
            float playerBottom = col.collider.bounds.min.y;
            float platformTop = GetComponent<Collider2D>().bounds.max.y;

            if (playerBottom >= platformTop - 0.05f)
            {
                triggered = true;

                // 🔊 проигрываем звук
                if (audioSource != null && stepClip != null)
                {
                    audioSource.PlayOneShot(stepClip);
                }

                Invoke(nameof(Fade), disappearTime);
            }
        }
    }

    void Fade()
    {
        gameObject.SetActive(false);
    }
} 