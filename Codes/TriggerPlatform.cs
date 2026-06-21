using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    [Header("Trigger replace platforms")]
    public bool isTriggerPlatform = false;
    public float triggerCooldown = 0.3f;

    private static float lastTriggerTime = -10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggerPlatform) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // только если игрок падает сверху
        if (collision.relativeVelocity.y >= 0) return;

        if (Time.time - lastTriggerTime < triggerCooldown) return;
        lastTriggerTime = Time.time;

        ReplaceAllPlatforms();
    }

    void ReplaceAllPlatforms()
    {
        PlatformManager manager = FindObjectOfType<PlatformManager>();
        if (manager != null)
            manager.ReplacePlatforms();
    }
}
