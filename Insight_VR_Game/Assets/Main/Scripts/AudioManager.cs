using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]List<AudioSource> backgroundAudios;
    float[] value = new float[2];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        value[0] = 1f;
        value[1] = 1f;
    }

    public void SetAudioSound()
    {
        StartCoroutine("AudioSoundDown");
    }

    IEnumerator AudioSoundDown()
    {
        float time = 0f;
        float fadeTime = 2.5f;

        while (value[0] > 0)
        {
            time += Time.deltaTime / fadeTime;
            value[0] = Mathf.Lerp(1, 0, time);
            value[1] = Mathf.Lerp(1, 0, time);

            backgroundAudios[0].volume = value[0];
            backgroundAudios[1].volume = value[1];

            yield return new WaitForSeconds(0.001f);
        }
    }
}
