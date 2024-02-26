using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class BarController : MonoBehaviour
{
    public static BarController instance;
    private void Awake()
    {
        instance = this;
    }
    

    public void OpenBar()
    {
        PlayerController.instance.RoundCount++;
        Debug.Log("라운드 카운트 = " + PlayerController.instance.RoundCount);
        GameObject Bar1;
        GameObject Bar2;
        GameObject Bar3;
        GameObject Bar4;

        Bar1 = Resources.Load("Prefabs/UI/Bar_1") as GameObject;
        Bar2 = Resources.Load("Prefabs/UI/Bar_2") as GameObject;
        Bar3 = Resources.Load("Prefabs/UI/Bar_3") as GameObject;
        Bar4 = Resources.Load("Prefabs/UI/Bar_4") as GameObject;

        Vector3 pos = new Vector3(57.1f, 5.7f, 57.55f);
        Quaternion rot = Quaternion.Euler(-20.0f, -115.0f, 0f);

        if(PlayerController.instance.RoundCount == 1)
        {
            GameObject bar1 = (GameObject)MonoBehaviour.Instantiate(Bar1);
            bar1.name = "Round1";
            bar1.transform.position = pos;
            bar1.transform.rotation = rot;
        }
        else if(PlayerController.instance.RoundCount == 2)
        {
            GameObject bar2 = (GameObject)MonoBehaviour.Instantiate(Bar2);
            bar2.name = "Round1";
            bar2.transform.position = pos;
            bar2.transform.rotation = rot;
        }
        else if( PlayerController.instance.RoundCount == 3)
        {
            GameObject bar3 = (GameObject)MonoBehaviour.Instantiate(Bar3);
            bar3.name = "Round1";
            bar3.transform.position = pos;
            bar3.transform.rotation = rot;
        }
        else if(PlayerController.instance.RoundCount == 4)
        {
            GameObject bar4 = (GameObject)MonoBehaviour.Instantiate(Bar4);
            bar4.name = "Round1";
            bar4.transform.position = pos;
            bar4.transform.rotation = rot;
        }
    }
}
