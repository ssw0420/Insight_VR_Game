using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHeal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats.Instance.Heal(1.0f); // Heal(1.0f)�ε� 2�� �� �Ǵ� ���� ����
        //Destroy(gameObject);
    }
}
