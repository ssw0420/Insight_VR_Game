using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager Instance;

    [SerializeField] List<AudioSource> backgroundAudioSource;

    private void Awake()
    {
        Instance = this;
        backgroundAudioSource.AddRange(gameObject.GetComponentsInChildren<AudioSource>());
    }

    private void Start()
    {
        backgroundAudioSource[0].Play();
    }

    public void StartRoundAudio()
    {
        switch (MonsterManager.Instance.GetStage())
        {
            case 2:
                backgroundAudioSource[1].Play();
                break;
            case 4:
                backgroundAudioSource[1].Stop();
                backgroundAudioSource[2].Play();
                break;
        }
    }
}
