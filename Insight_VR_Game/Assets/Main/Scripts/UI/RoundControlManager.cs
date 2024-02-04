using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundControlManager : MonoBehaviour
{
    public static RoundControlManager instance;
    private int CountNum = 1;

    GameObject Card_Prefab;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        Card_Prefab = Resources.Load("Prefabs/Card/GameCard") as GameObject;
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CountNum++;
        //    if (CountNum == RoundImage.instance.RoundNumber)
        //    {
        //        RoundImage.instance.ThisImg.sprite = RoundImage.instance.ChangeImg;
        //    }
        //    else if (CountNum == RoundImage.instance.RoundNumber + 1)
        //        RoundImage.instance.ThisImg.sprite = RoundImage.instance.EndImg;

        //    if (CountNum % 2 == 0)
        //        OpenCard(CountNum);
        //}
    }

    public void OpenCard(int Num)
    {
        GameObject card = MonoBehaviour.Instantiate(Card_Prefab);

        card.name = "Card";

        Vector3 pos = new Vector3(-0.267f, 2f, -0.64f);
        card.transform.position = pos;
    }
}
