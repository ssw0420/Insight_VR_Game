using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCard : MonoBehaviour
{
    GameObject Card_Prefab;
    public void Start()
    {
        Card_Prefab = Resources.Load("Prefabs/Card/GameCard") as GameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(Card_Prefab);
    }
}
