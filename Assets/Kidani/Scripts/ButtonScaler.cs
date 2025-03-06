using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f; // ägëÂî{ó¶
    public float transitionSpeed = 0.2f; // ägëÂÅEèkè¨ÇÃÉXÉsÅ[Éh

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    private System.Collections.IEnumerator ScaleButton(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;

        while (elapsedTime < transitionSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / transitionSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}