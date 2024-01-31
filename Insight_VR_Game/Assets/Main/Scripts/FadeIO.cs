using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIO : MonoBehaviour
{
    Image image;
    [SerializeField]float fadeTime = 1;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        Color color = image.color;
        float time = 0f;

        while (color.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            image.color = color;

            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator FadeOut()
    {
        Color color = image.color;
        float time = 0f;

        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            image.color = color;

            yield return new WaitForSeconds(0.001f);
        }
    }
}
