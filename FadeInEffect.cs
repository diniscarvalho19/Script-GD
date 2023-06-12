using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInEffect : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private Vector3 targetScale = Vector3.one;
    [SerializeField] private float fadeOutDelay = 2f;
    [SerializeField] private float fadeOutDuration = 1f;

    private CanvasGroup canvasGroup;
    private Vector3 startingScale;

    public void Activate()
    {
        gameObject.SetActive(true);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        startingScale = transform.localScale;
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        transform.localScale = targetScale;
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
            transform.localScale = Vector3.Lerp(targetScale, startingScale, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        transform.localScale = startingScale;
        gameObject.SetActive(false);
    }
}
