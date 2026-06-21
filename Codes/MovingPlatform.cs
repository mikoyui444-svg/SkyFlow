using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 2f;

    private Vector3 startPos;
    private Vector3 moveDir = Vector3.right;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + moveDir * Mathf.Sin(Time.time * speed) * distance;
    }
}
