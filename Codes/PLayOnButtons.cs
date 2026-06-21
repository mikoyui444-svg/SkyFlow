using UnityEngine;

public class PLayOnButtons : MonoBehaviour
{
    public static PLayOnButtons Instance;

    public AudioSource source;
    public AudioClip clickSoundButton;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayClick()
    {
        source.PlayOneShot(clickSoundButton);
    }
}

