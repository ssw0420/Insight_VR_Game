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
    public int CountNum = 1;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CountNum++;
            if (CountNum == RoundNumber)
            {
                ThisImg.sprite = ChangeImg;
            }
            else if (CountNum == RoundNumber + 1)
                ThisImg.sprite = EndImg;
            if (CountNum == RoundNumber)
                RoundControlManager.instance.OpenCard(CountNum);
            else if(CountNum == RoundNumber-1)
                GameCardDestroy.instance.CardDestroy();
        }
    }
}
