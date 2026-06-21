using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerCoins : MonoBehaviour
{
    public static GameManagerCoins Instance;

    public int coins;
    public TMP_Text coinsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateUI();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        coinsText.text = coins.ToString();
    }
}
