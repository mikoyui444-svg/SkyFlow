using UnityEngine;

public class PulseButton : MonoBehaviour
{
    public float speed = 2f;
    public float scaleAmount = 0.05f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        // 🔹 используем Mathf.Sin для плавного пульса
        float scale = 1f + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = startScale * scale;
    }
}
