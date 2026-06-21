using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioClip Clip;
    private Rigidbody2D rb;
    public float speed;
    public float forceJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))
        {
            GameManager.Instance.GameOver();
        }
    }
} 