using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPos;
    private bool moveRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (moveRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
            moveRight = !moveRight;
    }
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
