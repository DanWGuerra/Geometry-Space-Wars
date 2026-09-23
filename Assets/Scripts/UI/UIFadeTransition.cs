using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeTransition : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    public bool isFading = false;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeToBlack(System.Action onComplete = null)
    {
        Debug.Log("FadeToBlack called, isFading = " + isFading + ", gameObject active = " + gameObject.activeInHierarchy);
        if (!isFading)
            StartCoroutine(FadeRoutine(1f, onComplete));
        Debug.Log("Executing");
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
            time += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
        isFading = false;

        onComplete?.Invoke();
    }

    void OnDisable()
    {
        // Safety net: if this object gets disabled mid-fade, don't leave isFading stuck
        isFading = false;
        StopAllCoroutines();
    }
}
