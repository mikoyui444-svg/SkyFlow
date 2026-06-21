using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int amount = value;

            // 🔥 БУСТ x2 МОНЕТ
            if (BoostManager.Instance != null && BoostManager.Instance.doubleCoins)
                amount *= 2;

            GameManagerCoins.Instance.AddCoins(amount);

            if (pickupSound != null && audioSource != null)
                audioSource.PlayOneShot(pickupSound);

            // удаляем монету после проигрывания звука
            Destroy(gameObject, 0.2f);
        }
    }
}
