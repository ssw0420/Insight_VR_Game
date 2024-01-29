using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIO : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gameObject.transform.parent);
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

        for(int i = 0; i <= 100; i++)
        {
            color.a += Time.deltaTime * 0.01f;
            image.color = color;

            if (image.color.a >= 1)
                break;
        }
        yield return null;
    }

    IEnumerator FadeOut()
    {
        yield return null;
    }
}
