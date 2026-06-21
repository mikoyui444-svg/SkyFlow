using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.1f;  // увеличение при наведении
    public float clickScale = 0.9f;  // уменьшение при клике
    public float smooth = 10f;       // скорость анимации

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // плавно возвращаемся к оригинальному размеру
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * smooth);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = originalScale * clickScale;
    }
} 