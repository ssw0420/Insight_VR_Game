using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]FadeIO fade;
    protected static SceneLoader instance;

    public static SceneLoader Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();
                if (obj != null)
                    instance = obj;
                else
                    instance = Create();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    bool isSceneMove = false;

    public static SceneLoader Create()
    {
        var SceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
        return Instantiate(SceneLoaderPrefab);
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        fade = GameObject.Find("FadeIO").GetComponent<FadeIO>();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if (isSceneMove)
            return;

        isSceneMove = true;
        //gameObject.SetActive(true);
        StartCoroutine(Load(sceneName));

        AudioManager.instance.SetAudioSound();
    }

    IEnumerator Load(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        fade.StartFadeIn();
        yield return new WaitForSeconds(2.5f);
        
        async.allowSceneActivation = true;
        fade = GameObject.Find("FadeIO").GetComponent<FadeIO>();
        fade.StartFadeOut();
        isSceneMove = false;
    }
}
