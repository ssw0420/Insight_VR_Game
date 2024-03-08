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

    public void ShowCard()
    {
        CardControlManager.instance.OpenCard();
        BarController.instance.OpenBar();
    }
}
