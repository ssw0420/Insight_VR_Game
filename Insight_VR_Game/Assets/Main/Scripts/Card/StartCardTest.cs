using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCardTest : MonoBehaviour
{
    public static StartCardTest instance;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CardControlManager.instance.OpenCard();
        }
    }
}
