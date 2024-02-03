using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundControlManager : MonoBehaviour
{
    public static RoundControlManager instance;
    private int CountNum = 1;


    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CountNum++;
            if (CountNum == RoundImage.instance.RoundNumber)
                RoundImage.instance.ThisImg.sprite = RoundImage.instance.ChangeImg;
            else if (CountNum == RoundImage.instance.RoundNumber + 1)
                RoundImage.instance.ThisImg.sprite = RoundImage.instance.EndImg;

            if (CountNum % 2 == 0)
                OpenCard(CountNum);
        }
    }

    public void OpenCard(int Num)
    {

    }
}
