using UnityEngine;

public class ShieldPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerShield shield = collision.gameObject.GetComponent<PlayerShield>();
            if (shield != null)
            {
                shield.ActivateShield();
            }
            Destroy(gameObject);
        }
    }
}