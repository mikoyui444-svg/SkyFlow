using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float yOffset = 1f; // смещение вверх, чтобы игрок не залипал в коллайдере

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Обновляем точку возрождения с небольшим смещением по Y
            PlayerHealth.Instance.UpdateRespawnPoint(transform.position + new Vector3(0, yOffset, 0));
        }
    }
}
