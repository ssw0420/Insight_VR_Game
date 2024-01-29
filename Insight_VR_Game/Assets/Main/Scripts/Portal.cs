using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameManager gameManager;

    void MoveStageScene()
    {
        gameManager.SceneMove(1);
    }
}
