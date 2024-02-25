using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundControlManager : MonoBehaviour
{
    public static RoundControlManager instance;
    //private int CountNum = 1;
    public Sprite ChangeImg;
    public Sprite EndImg;
    public int RoundNumber = 0;
    public Image ThisImg;
    public int CountNum = 0;
    //GameObject Card_Prefab;
    //GameObject Card_BlackHole;
    //GameObject Card_Ice;
    //GameObject Card_Upgrade1;
    //GameObject Card_Upgrade2;
    int RoundCount = 1;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        //Card_Prefab = Resources.Load("Prefabs/Card/GameCard") as GameObject;
        //Card_BlackHole = Resources.Load("Prefabs/Card/GameCard_Skill_blackHole") as GameObject;
        //Card_Ice = Resources.Load("Prefabs/Card/GameCard_Skill_ice") as GameObject;
        //Card_Upgrade1 = Resources.Load("Prefabs/Card/GameCard_Upgrade_1") as GameObject;
        //Card_Upgrade2 = Resources.Load("Prefabs/Card/GameCard_Upgrade_2") as GameObject;

        //if (CountNum == RoundCount)
        //{
        //    GameObject card_blackhole = (GameObject)MonoBehaviour.Instantiate(Card_BlackHole);
        //    GameObject card_ice = (GameObject)MonoBehaviour.Instantiate(Card_Ice);
        //    GameObject card_upgrade1 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade1);
        //    GameObject card_upgrade2 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade2);

        //    card_blackhole.name = "card_blackhole";
        //    card_ice.name = "card_ice";
        //    card_upgrade1.name = "card_upgrade1";
        //    card_upgrade2.name = "card_upgrade2";

        //    Vector3 pos1 = new Vector3(64.323f, 3.087f, 58.698f);
        //    Vector3 pos2 = new Vector3(64.003f, 3.092f, 59.662f);
        //    Vector3 pos3 = new Vector3(63.636f, 3.083f, 60.602f);
        //    Vector3 pos4 = new Vector3(63.308f, 3.083f, 61.535f);
            
        //    card_blackhole.transform.position = pos1;
        //    card_ice.transform.position = pos2;
        //    card_upgrade1.transform.position = pos3;
        //    card_upgrade2.transform.position = pos4;

        //    card_blackhole.transform.rotation = Quaternion.Euler(0f, -110.0f, 0f);
        //    card_ice.transform.rotation = Quaternion.Euler(0f, -110.0f, 0f);
        //    card_upgrade1.transform.rotation = Quaternion.Euler(0f, -110.0f, 0f);
        //    card_upgrade2.transform.rotation = Quaternion.Euler(0f, -110.0f, 0f);

        //    RoundCount++;
        //}
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CountNum++;
            RoundImage.instance.ChangeImgs();
            //RoundNumber++;
            if (CountNum - 1 == RoundNumber)
                OpenCard(CountNum);
            //else if (CountNum == RoundNumber - 1)
            //    GameCardDestroy.instance.CardDestroy();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "card_blackhole")
        {
            Debug.Log("블랙홀");
            Destroy(gameObject);
        }
        if (other.name == "card_ice")
        {
            Debug.Log("아이스");
            Destroy(gameObject);
        }
        if (other.name == "card_upgrade1")
        {
            Debug.Log("업글1");
            Destroy(gameObject);
        }
        if (other.name == "card_upgrade2")
        {
            Debug.Log("업글2");
            Destroy(gameObject);
        }

    }

    public void OpenCard(int Num)
    {
        //GameObject card = (GameObject)MonoBehaviour.Instantiate(Card_Prefab);
        //Debug.Log("카운트 넘버 : " + CountNum);
        //Debug.Log("라운드 넘버 : " + RoundImage.instance.RoundNumber);
        //card.name = "Card";

        //Vector3 pos = new Vector3(63.0f, 3f, 62.0f);
        //card.transform.position = pos;
    }
}
