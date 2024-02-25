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

    public int RoundCount = 0;
}
