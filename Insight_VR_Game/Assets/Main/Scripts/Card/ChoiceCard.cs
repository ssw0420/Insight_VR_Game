using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCard : MonoBehaviour
{
    //GameObject Card_BlackHole;
    //GameObject Card_Ice;
    //GameObject Card_Upgrade1;
    //GameObject Card_Upgrade2;
    //int RoundCount = 1;
    public void Start()
    {
        //Card_BlackHole = Resources.Load("Prefabs/Card/GameCard_Skill_blackHole") as GameObject;
        //Card_Ice = Resources.Load("Prefabs/Card/GameCard_Skill_ice") as GameObject;
        //Card_Upgrade1 = Resources.Load("Prefabs/Card/GameCard_Upgrade_1") as GameObject;
        //Card_Upgrade2 = Resources.Load("Prefabs/Card/GameCard_Upgrade_2") as GameObject;

        //if (RoundControlManager.instance.CountNum == RoundCount)
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
        //    RoundCount++;
        //}
    }
    //private void Update()
    //{
    //    if(RoundControlManager.instance.CountNum == RoundCount)
    //    {
    //        GameObject card_blackhole = (GameObject)MonoBehaviour.Instantiate(Card_BlackHole);
    //        GameObject card_ice = (GameObject)MonoBehaviour.Instantiate(Card_Ice);
    //        GameObject card_upgrade1 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade1);
    //        GameObject card_upgrade2 = (GameObject)MonoBehaviour.Instantiate(Card_Upgrade2);

    //        card_blackhole.name = "card_blackhole";
    //        card_ice.name = "card_ice";
    //        card_upgrade1.name = "card_upgrade1";
    //        card_upgrade2.name = "card_upgrade2";

    //        Vector3 pos1 = new Vector3(64.323f, 3.087f, 58.698f);
    //        Vector3 pos2 = new Vector3(64.003f, 3.092f, 59.662f);
    //        Vector3 pos3 = new Vector3(63.636f, 3.083f, 60.602f);
    //        Vector3 pos4 = new Vector3(63.308f, 3.083f, 61.535f);
    //        card_blackhole.transform.position = pos1;
    //        card_ice.transform.position = pos2;
    //        card_upgrade1.transform.position = pos3;
    //        card_upgrade2.transform.position = pos4;
    //        RoundCount++;
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
        
    //    //Destroy(gameObject); // ī�忡 ������ ��ų�̳� ��ȭ�� ���ְ� Destroy ���ֱ�
        
    //    //Destroy(Card_Prefab); // ī�带 �����ϸ� ��ü ī�带 ���� �� ��� �Ǵµ� prefab ��ü�� ������ �� �ֱ� ������ ��ü�� ���� ����� �༭
    //                          // ������ ������ destroy ���ֱ�
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Destroy(gameObject);
        if (other.name == "card_blackhole")
        {
            Debug.Log("��Ȧ");
            Destroy(gameObject);
        }
        else if (other.name == "card_ice")
        {
            Debug.Log("���̽�");
            Destroy(gameObject);
        }
        else if (other.name == "card_upgrade1")
        {
            Debug.Log("����1");
            Destroy(gameObject);
        }
        else if (other.name == "card_upgrade2")
        {
            Debug.Log("����2");
            Destroy(gameObject);
        }

    }
}
