using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardDestroy : MonoBehaviour
{
    public static GameCardDestroy instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CardDestroy()
    {
        Destroy(gameObject);
    }
}
