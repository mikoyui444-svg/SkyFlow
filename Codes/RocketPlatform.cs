using UnityEngine;

public class RocketPlatform : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float rocketForce = 20f; // сила подбрасывания

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip rocketClip;

    private bool triggered = false; // чтобы не срабатывало несколько раз за одно касание

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (triggered)
            return;

        if (!col.collider.CompareTag("Player"))
            return;

        Rigidbody2D playerRb = col.collider.GetComponent<Rigidbody2D>();
        if (playerRb == null)
            return;

        // 🔹 проверяем, что игрок падает вниз
        if (playerRb.velocity.y <= 0f)
        {
            // 🔹 проверяем, что игрок сверху платформы
            float playerBottom = col.collider.bounds.min.y;
            float platformTop = GetComponent<Collider2D>().bounds.max.y;

            if (playerBottom >= platformTop - 0.05f)
            {
                triggered = true;

                // 🔹 подбрасываем игрока
                playerRb.velocity = new Vector2(playerRb.velocity.x, rocketForce);

                // 🔊 проигрываем звук ракеты
                if (audioSource != null && rocketClip != null)
                {
                    audioSource.PlayOneShot(rocketClip);
                }

                // 🔹 можно добавить визуальный эффект ракеты здесь
                // Instantiate(rocketEffectPrefab, transform.position, Quaternion.identity);

                // 🔹 сброс триггера через 0.5 секунды, чтобы игрок мог использовать платформу снова
                Invoke(nameof(ResetTrigger), 0.5f);
            }
        }
    }

    private void ResetTrigger()
    {
        triggered = false;
    }
}
