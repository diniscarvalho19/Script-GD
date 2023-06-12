using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTime : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDelay = 2f;
    [SerializeField] private float fadeOutDuration = 1f;

    private CanvasGroup canvasGroup;

    public void Activate()
    {
        gameObject.SetActive(true);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(fadeOutDelay);
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
