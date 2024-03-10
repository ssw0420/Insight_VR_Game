using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;

    AudioSource musicAudio;

    [Header("UI Scripts")]
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject loseUI;

    [Header("Audio Source")]
    public AudioClip victoryAudio;
    public AudioClip loseAudio;

    [Header("Timer")]
    [SerializeField]bool isTimer = false;
    [SerializeField]float playTime;

    [Header("Weapon Object")]
    [SerializeField] GameObject crossbow;
    [SerializeField] GameObject skillBall;

    bool isEnd = false;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        musicAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isTimer)
            playTime += Time.deltaTime;
    }

    public void StartTimeCount()
    {
        isTimer = true;
    }

    public void PauseTimeCount()
    {
        isTimer = false;
    }

    public void GameVictroy()
    {
        if (isEnd)
            return;
        isEnd = true;

        //Timer Stop
        isTimer = false;

        //Victory Music Play
        BgmManager.Instance.VictoryAudio();
        musicAudio.clip = victoryAudio;
        musicAudio.Play();

        //Game End UI Setting
        victoryUI.GetComponent<GameEndUI>().OnEndUI();
        victoryUI.GetComponent<GameEndUI>().ClearTimeText(playTime);

        //Controller Setting
        ControllerSetting();

        //Time Reset
        playTime = 0;
    } 

    public void GameLose()
    {
        if (isEnd)
            return;
        isEnd = true;

        //Timer Stop
        isTimer = false;

        //Lose Music Play
        musicAudio.clip = loseAudio;
        musicAudio.Play();

        //Game End UI Setting
        loseUI.GetComponent<GameEndUI>().OnEndUI();

        //Monster Victory Animation
        MonsterManager.Instance.MonsterWin();

        //Controller Setting
        ControllerSetting();

        //Time Reset
        playTime = 0;
    }

    void ControllerSetting()
    {
        Destroy(crossbow);
        Destroy(skillBall);

        GameObject rightController = GameObject.Find("Right Controller");
        rightController.GetComponent<LineRenderer>().enabled = true;
    }
}