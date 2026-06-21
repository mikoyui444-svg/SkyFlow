using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public float healAmount = 100f; 
    public AudioClip pickupSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.TakeDamage(-healAmount); // восстановление здоровья

                // 🔊 Проигрываем звук
                if (pickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                // Удаляем объект после короткой задержки, чтобы звук успел проиграться
                Destroy(gameObject, 0.2f); // задержка 0.2 секунды
            }
        }
    }
}
