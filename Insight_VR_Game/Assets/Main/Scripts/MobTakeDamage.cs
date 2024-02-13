using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTakeDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats.Instance.TakeDamage(1.0f);
    }
}
