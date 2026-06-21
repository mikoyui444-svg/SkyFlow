using UnityEngine;
using System.Collections;

public class NotEnoughCoinsFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration = 1.5f;
    private Coroutine fadeCoroutine;

    public void Show()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(duration);

        float elapsed = 0f;
        float fadeTime = 0.5f;
        while (elapsed < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        fadeCoroutine = null;
    }
}
