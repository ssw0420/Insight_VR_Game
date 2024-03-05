using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    Button RestartButton;
    Button HomeButton;

    public void OnEndUI()
    {
        gameObject.SetActive(true);
    }

    public void ClearTimeText(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);
        Text clearTime = gameObject.GetComponentsInChildren<Text>()[2];
        clearTime.text = minute + " : " + second;
    }

    public void ClickHome()
    {
        Debug.Log("Home");
        SceneTransitionManager.Instance.GoToSceneAsync("Home");
    }

    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
