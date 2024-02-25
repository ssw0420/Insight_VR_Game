using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string SceneName;

    public void MoveStageScene()
    {
        SceneLoader.Instance.LoadScene(SceneName);
    }
}
