using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControlManager : MonoBehaviour
{
    public static CardControlManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
        //GameObject Card_BlackHole;
        //GameObject Card_Ice;
        //GameObject Card_Upgrade1;
        //GameObject Card_Upgrade2;

        //public void Start()
        //{
        //    Card_BlackHole = Resources.Load("Prefabs/Card/GameCard_Skill_blackHole") as GameObject;
        //    Card_Ice = Resources.Load("Prefabs/Card/GameCard_Skill_ice") as GameObject;
        //    Card_Upgrade1 = Resources.Load("Prefabs/Card/GameCard_Upgrade_1") as GameObject;
        //    Card_Upgrade2 = Resources.Load("Prefabs/Card/GameCard_Upgrade_2") as GameObject;
        //}
    public void OpenCard()
    {
        Debug.Log("opencard ½ÇÇà");
        GameObject Card_BlackHole;
        GameObject Card_Ice;
        GameObject Card_Upgrade1;
        GameObject Card_Upgrade2;

        GameObject Card_BlackHole_no;
        GameObject Card_Ice_no;
        GameObject Card_Upgrade1_no;
        GameObject Card_Upgrade2_no;




        Card_BlackHole = Resources.Load("Prefabs/Card/GameCard_Skill_blackHole_yes") as GameObject;
        Card_Ice = Resources.Load("Prefabs/Card/GameCard_Skill_ice_yes") as GameObject;
        Card_Upgrade1 = Resources.Load("Prefabs/Card/GameCard_Upgrade_1_yes") as GameObject;
        Card_Upgrade2 = Resources.Load("Prefabs/Card/GameCard_Upgrade_2_yes") as GameObject;

        Card_BlackHole_no = Resources.Load("Prefabs/Card/GameCard_Skill_blackHole_no") as GameObject;
        Card_Ice_no = Resources.Load("Prefabs/Card/GameCard_Skill_ice_no") as GameObject;
        Card_Upgrade1_no = Resources.Load("Prefabs/Card/GameCard_Upgrade_1_no") as GameObject;
        Card_Upgrade2_no = Resources.Load("Prefabs/Card/GameCard_Upgrade_2_no") as GameObject;

        Vector3 pos1 = new Vector3(64.1f, 3.087f, 58.698f);
        Vector3 pos2 = new Vector3(63.764f, 3.083f, 59.662f);
        Vector3 pos3 = new Vector3(63.419f, 3.083f, 60.602f);
        Vector3 pos4 = new Vector3(63.076f, 3.083f, 61.535f);
        Quaternion rot = Quaternion.Euler(0f, -110.0f, 0f);


        if (PlayerController.instance.BlackHoleState == false)
        {
            GameObject card_blackhole = (GameObject)MonoBehaviour.Instantiate(Card_BlackHole);
            card_blackhole.name = "card_blackhole";
            card_blackhole.transform.position = pos1;
            card_blackhole.transform.rotation = rot;
        }
        else if(PlayerController.instance.BlackHoleState == true)
        {
            GameObject card_blackhole_no = (GameObject)MonoBehaviour.Instantiate(Card_BlackHole_no);
            card_blackhole_no.name = "card_blackhole_no";
            card_blackhole_no.transform.position = pos1;
            card_blackhole_no.transform.rotation = rot;
        }
        
        if(PlayerController.instance.IceState == false)
        {
            GameObject card_ice = (GameObject)MonoBehaviour.Instantiate(Card_Ice);
            card_ice.name = "card_ice";
            card_ice.transform.position = pos2;
            card_ice.transform.rotation = rot;
        }
        else if( PlayerController.instance.IceState == true)
        {
            GameObject card_ice_no = (GameObject)MonoBehaviour.Instantiate(Card_Ice_no);
            card_ice_no.name = "card_ice_no";
            card_ice_no.transform.position = pos2;
            card_ice_no.transform.rotation = rot;
        }

        if( PlayerController.instance.DmgState == false)
        {
            GameObject card_upgrade1 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade1);
            card_upgrade1.name = "card_upgrade1";
            card_upgrade1.transform.position = pos3;
            card_upgrade1.transform.rotation = rot;
        }
        else if(PlayerController.instance.DmgState == true)
        {
            GameObject card_upgrade1_no = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade1_no);
            card_upgrade1_no.name = "card_upgrade1_no";
            card_upgrade1_no.transform.position = pos3;
            card_upgrade1_no.transform.rotation = rot;
        }
        
        if(PlayerController.instance.HealthState == false)
        {
            GameObject card_upgrade2 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade2);
            card_upgrade2.name = "card_upgrade2";
            card_upgrade2.transform.position = pos4;
            card_upgrade2.transform.rotation = rot;
        }
        else if(PlayerController.instance.HealthState == true)
        {
            GameObject card_upgrade2_no = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade2_no);
            card_upgrade2_no.name = "card_upgrade2_no";
            card_upgrade2_no.transform.position = pos4;
            card_upgrade2_no.transform.rotation = rot;
        }
    }
}


