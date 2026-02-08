using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeTransition : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    bool isFading = false;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeToBlack(System.Action onComplete = null)
    {
        if (!isFading)
            StartCoroutine(FadeRoutine(1f, onComplete));
    }

    public void FadeFromBlack()
    {
        if (!isFading)
            StartCoroutine(FadeRoutine(0f, null));
    }

    IEnumerator FadeRoutine(float targetAlpha, System.Action onComplete)
    {
        isFading = true;

        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
        isFading = false;

        onComplete?.Invoke();
    }
}
