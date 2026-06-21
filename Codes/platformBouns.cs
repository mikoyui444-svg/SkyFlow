using System.Diagnostics;
using UnityEngine;

public class platformBouns : MonoBehaviour
{
    public float Jumpforce = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.y < 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * Jumpforce;
            }
        }
    }
}
