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
        Destroy(gameObject); // 카드에 정해진 스킬이나 강화를 해주고 Destroy 해주기
        Destroy(Card_Prefab); // 카드를 선택하면 전체 카드를 삭제 해 줘야 되는데 prefab 자체를 삭제할 수 있기 때문에 객체를 따로 만들어 줘서
                              // 참조한 것으로 destroy 해주기
    }
}
