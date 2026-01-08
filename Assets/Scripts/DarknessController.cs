using UnityEngine;
using System.Collections;

public class DarknessController : MonoBehaviour
{
    public SpriteRenderer darknessSprite; 
    public float fadeSpeed = 2.0f; // Zwiększmy do 2 sekund, żeby lepiej widzieć test

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) 
        {
            Debug.Log("Lis wszedł do zamku - start ściemniania");
            StartFade(1.0f); // Docelowo czarny
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Lis wyszedł z zamku - start rozjaśniania");
            StartFade(0.0f); // Docelowo przezroczysty
        }
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        if (darknessSprite == null) yield break;

        Color color = darknessSprite.color;
        float startAlpha = color.a;
        float timer = 0;

        while (timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            // Lerp płynnie przechodzi między startAlpha a targetAlpha
            color.a = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeSpeed);
            darknessSprite.color = color;
            yield return null; 
        }

        color.a = targetAlpha;
        darknessSprite.color = color;
    }
}