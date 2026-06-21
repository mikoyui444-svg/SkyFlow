using UnityEngine;
using TMPro; // если используешь TextMeshPro

public class BoostShop : MonoBehaviour
{
    public int doubleCoinsPrice = 200;
    public int shieldPrice = 150;
    public int jumpBoostPrice = 150;

    public TMP_Text notEnoughCoinsText; // ссылка на текст

    public void BuyDoubleCoins()
    {
        if (GameManagerCoins.Instance.SpendCoins(doubleCoinsPrice))
        {
            BoostManager.Instance.doubleCoins = true;
            Debug.Log("x2 Coins куплен");
        }
        else
        {
            ShowNotEnoughCoins();
        }
    }

    public void BuyShield()
    {
        if (GameManagerCoins.Instance.SpendCoins(shieldPrice))
        {
            BoostManager.Instance.shieldBought = true;
            BoostManager.Instance.shield = true;
            Debug.Log("Щит куплен, активируется при старте уровня");
        }
        else
        {
            ShowNotEnoughCoins();
        }
    }

    public void BuyJumpBoost()
    {
        if (GameManagerCoins.Instance.SpendCoins(jumpBoostPrice))
        {
            BoostManager.Instance.jumpBoost = true;
            Debug.Log("Буст прыжка куплен, активируется при старте уровня");
        }
        else
        {
            ShowNotEnoughCoins();
        }
    }

    void ShowNotEnoughCoins()
    {
        if (notEnoughCoinsText != null)
        {
            notEnoughCoinsText.gameObject.SetActive(true);
            CancelInvoke("HideNotEnoughCoins"); // на случай, если уже был вызов
            Invoke("HideNotEnoughCoins", 1.5f); // скрываем через 1.5 сек
        }
    }

    void HideNotEnoughCoins()
    {
        if (notEnoughCoinsText != null)
            notEnoughCoinsText.gameObject.SetActive(false);
    }
} 