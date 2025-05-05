using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : LinkMonoBehaviour
{
    public SpriteRenderer wallRenderer;
    public float fadeDuration = 0.5f;
    public float targetAlpha = 0.3f;

    private Coroutine fadeCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartFade(targetAlpha);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartFade(1f); // về lại alpha gốc
        }
    }

    void StartFade(float newAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToAlpha(newAlpha));
    }

    System.Collections.IEnumerator FadeToAlpha(float alpha)
    {
        Color color = wallRenderer.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, alpha, t);
            wallRenderer.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = alpha;
        wallRenderer.color = color;
    }
}
