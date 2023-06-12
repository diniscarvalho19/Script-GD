using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float duration = 2f; // The duration of the fade-out effect
    public RawImage rawImage; // The UI RawImage to fade out

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = rawImage.GetComponent<CanvasGroup>();
        StartCoroutine(FadeOutRawImage());
    }

    public void StayActive(){
        while(!rawImage.gameObject.activeSelf){
            rawImage.gameObject.SetActive(true);
        }
    }

    private IEnumerator FadeOutRawImage()
    {
        float currentTime = 0f;
        float startingAlpha = canvasGroup.alpha;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(startingAlpha, 0f, currentTime / duration);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // Ensure that the alpha value is set to 0 at the end of the effect
        rawImage.gameObject.SetActive(false); // Disable the UI RawImage
    }

    public void FadeIn(){
        StartCoroutine(FadeInRawImage());
    }

    private IEnumerator FadeInRawImage()
    {
        float currentTime = 0f;
        float startingAlpha = canvasGroup.alpha;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(startingAlpha, 1f, currentTime / duration);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure that the alpha value is set to 1 at the end of the effect
    }




}
