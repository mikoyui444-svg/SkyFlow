using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    Rigidbody2D rb;
    float halfWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // вычисляем половину ширины камеры один раз
        Camera cam = Camera.main;
        if (cam != null)
            halfWidth = cam.orthographicSize * cam.aspect;
        else
            halfWidth = 5f; // запасное значение
    }

    void LateUpdate()
    {
        WrapAround();
    }

    void WrapAround()
    {
        Vector2 pos = rb.position;

        if (pos.x < -halfWidth) pos.x = halfWidth;
        else if (pos.x > halfWidth) pos.x = -halfWidth;

        rb.position = pos;
    }
}
