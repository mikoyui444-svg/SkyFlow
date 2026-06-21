using UnityEngine;

public class DoubleCoinsUI : MonoBehaviour
{
    public GameObject doubleCoinsIcon;

    void Start()
    {
        doubleCoinsIcon.SetActive(false);
    }

    void Update()
    {
        if (BoostManager.Instance != null)
        {
            doubleCoinsIcon.SetActive(BoostManager.Instance.doubleCoins);
        }
    }
}
