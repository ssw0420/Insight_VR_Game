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
        Destroy(gameObject); // ī�忡 ������ ��ų�̳� ��ȭ�� ���ְ� Destroy ���ֱ�
        Destroy(Card_Prefab); // ī�带 �����ϸ� ��ü ī�带 ���� �� ��� �Ǵµ� prefab ��ü�� ������ �� �ֱ� ������ ��ü�� ���� ����� �༭
                              // ������ ������ destroy ���ֱ�
    }
}
