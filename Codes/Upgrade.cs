using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Upgrade")]
    public string productKey = "Player Speed";
    public int upgradeLimit = 5;
    public int price = 100;

    [Header("UI")]
    public Image[] emptyIcons;
    public Sprite fillIcon;

    void Start()
    {
        UpdateIcons();
    }

    public void BuyUpgrade()
    {
        int level = PlayerPrefs.GetInt(productKey, 0);

        if (level >= upgradeLimit)
            return;

        if (!GameManagerCoins.Instance.SpendCoins(price))
            return;

        level++;
        PlayerPrefs.SetInt(productKey, level);

        UpdateIcons();

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
            player.StatsUpdate();
    }

    void UpdateIcons()
    {
        int level = PlayerPrefs.GetInt(productKey, 0);

        for (int i = 0; i < emptyIcons.Length; i++)
        {
            emptyIcons[i].overrideSprite = i < level ? fillIcon : null;
        }
    }
}
