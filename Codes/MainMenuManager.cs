using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text mainTextCoins;

    private void Update()
    {
        mainTextCoins.text = GameManagerCoins.Instance.coins.ToString();
    }
}
