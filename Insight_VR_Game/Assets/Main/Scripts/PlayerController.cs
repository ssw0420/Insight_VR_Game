using MagicPigGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool BlackHoleState = false;
    public bool IceState = false;
    public bool DmgState = false;
    public bool HealthState = false;

    public int RoundCount = 1;

    GameObject HealthBar1;
    GameObject HealthBar2;

    private void Update()
    {
        if (HealthState)
        {
            HealthBar1 = GameObject.Find("Border Mask");
            HealthBar2 = GameObject.Find("Border");
            HealthBar1.transform.localScale = new Vector3(2.0f, 1.0f, 1.0f);
            HealthBar2.transform.localScale = new Vector3(2.0f, 1.0f, 1.0f);
        }
    }
}
