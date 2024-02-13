/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using Unity.VisualScripting;
using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    //public void AddHealth()
    //{
    //    PlayerStats.Instance.AddHealth();
    //}
    //public void Heal(float health)
    //{
    //    PlayerStats.Instance.Heal(health);
    //}
    //public void Hurt(float dmg)
    //{
    //    PlayerStats.Instance.TakeDamage(dmg);
    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            PlayerStats.Instance.Heal(1.0f);
        if (Input.GetKeyDown(KeyCode.X))
            PlayerStats.Instance.TakeDamage(1.0f);
    }
}
