using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    Camera cam;
    float screenBottom;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // нижняя граница экрана в мировых координатах
        screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.y < screenBottom - 1f)
        {
            Destroy(gameObject);
        }
    }
}

