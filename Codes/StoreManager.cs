using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public GameObject shopPanel; // Панель магазина
    public TMP_Text coinsText;
    private void Update()
    {
        coinsText.text = GameManagerCoins.Instance.coins.ToString();
    }

    private void Start()
    {
        shopPanel.SetActive(false); // По умолчанию закрыта
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true); // Открыть
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false); // Закрыть
    }
}
