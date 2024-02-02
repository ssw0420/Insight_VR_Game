using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    private Animator animator;
    public float MaxHP = 3.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
 

    void Update()
    {
        this.MaxHP -= Time.deltaTime;
        if(this.MaxHP >= 0)
        {
            animator.SetTrigger("Hit Trigger");
        }
        else
        {
            animator.SetBool("Die Bool", true);
        }

        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    PlayerStats.Instance.AddHealth();
        //}

        //if (Input.GetKeyUp(KeyCode.Return))
        //{
        //    PlayerStats.Instance.Heal(health);
        //}

        //if (Input.GetKeyUp(KeyCode.Return))
        //{
        //    PlayerStats.Instance.TakeDamage(dmg);
        //}
    }
}
