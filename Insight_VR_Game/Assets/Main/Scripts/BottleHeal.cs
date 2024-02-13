using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHeal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats.Instance.Heal(1.0f); // Heal(1.0f)인데 2번 힐 되는 오류 있음
        //Destroy(gameObject);
    }
}
