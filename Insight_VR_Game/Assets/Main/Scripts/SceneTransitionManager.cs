using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public FadeScreen fadeScreen;
    bool isSceenMove = false;

    public void GoToSceneAsync(string sceneName)
    {
        if(!isSceenMove)
        {
            StartCoroutine(GoToSceneAsyncRoutine(sceneName));
        }
    }

    IEnumerator GoToSceneAsyncRoutine(string sceneName)
    {

        isSceenMove=true;
        fadeScreen.FadeOut();

        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(timer <= fadeScreen.fadeDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
