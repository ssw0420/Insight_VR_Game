using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponManager : MonoBehaviour
{
    public XRBaseInteractor controller;

    [Header("Weapon Object")]
    [SerializeField] GameObject iceBall;
    [SerializeField] GameObject blackHole;

    //skillType = 1 : iceBall
    //skillType = 2 : blackHole
    int skillType;

    private void Start()
    {
        skillType = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            ChangeSkill();
        }
    }

    void ChangeSkill()
    {
        if (skillType == 1)
            skillType = 2;
        else
            skillType = 1;

        switch (skillType) {
            case 1:
                OnIceBall();
                break;
            case 2:
                OnBlackHole();
                break;
        }
    }

    void OnIceBall()
    {
        Debug.Log("얼음 스킬 소환");
        blackHole.SetActive(false);
        iceBall.SetActive(true);

        iceBall.transform.position = controller.transform.position;
        iceBall.transform.rotation = controller.transform.rotation;
    }

    void OnBlackHole()
    {
        Debug.Log("블랙홀 스킬 소환");
        iceBall.SetActive(false);
        blackHole.SetActive(true);
        //controller.useForceGrab = blackHole;
    }

    public void GrabSkill()
    {

    }
}
