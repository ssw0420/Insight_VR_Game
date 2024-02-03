using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundImage : MonoBehaviour
{
    public static RoundImage instance;
    public Sprite ChangeImg;
    public Sprite EndImg;
    public int RoundNumber = 0;
    public Image ThisImg;
    private int count = 1;
    
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        ThisImg = GetComponent<Image>();
    }
 
    void Update()
    {
       
    }
}
