using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager Instance;
    public bool shieldBought = false;


    [Header("Boosts")]
    public bool doubleCoins;
    public bool extraLife;
    public bool shield;
    public bool jumpBoost;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ResetBoosts()
    {
        doubleCoins = false;
        extraLife = false;
        shield = false;
        jumpBoost = false;
    }
} 