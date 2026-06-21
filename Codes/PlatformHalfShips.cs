using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHalfShips : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(50f);
        }
    }
}
}
