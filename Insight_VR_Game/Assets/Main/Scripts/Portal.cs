using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void MoveStageScene()
    {
        SceneLoader.Instance.LoadScene("MoodyNight");
    }
}
