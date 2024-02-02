using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] int stage = 1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int GetStage()
    {
        return stage;
    }
}