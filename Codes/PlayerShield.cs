using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerShield : MonoBehaviour
{
    public bool isShieldActive;
    public float shieldDuration = 10f;

    public GameObject shieldVisual;
    public TMP_Text shieldTimerText; // UI текст

    private Coroutine shieldCoroutine;

    void Start()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(false);
        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(false);

          // 🔹 активируем купленный щит при старте уровня
        if (BoostManager.Instance != null && BoostManager.Instance.shieldBought)
        {
           shieldDuration = 30f;
           ActivateShield();
           BoostManager.Instance.shieldBought = false; // сбрасываем флаг
        }
    }

    public void ActivateShield()
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldTimer());
    }

    IEnumerator ShieldTimer()
    {
        isShieldActive = true;
        float timeLeft = shieldDuration;

        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            if (shieldTimerText != null)
                shieldTimerText.text = "" + Mathf.Ceil(timeLeft);

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        isShieldActive = false;

        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(false);
    }
}
