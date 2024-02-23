using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonster : Monster
{
    GameObject HealthPotionPrefab;

    protected override void Start()
    {
        base.Start();
        HealthPotionPrefab = MonsterManager.Instance.GetHealthPotionPrefab();
    }

    protected override void Die()
    {
        base.Die();
        GameObject HealthPotion = Instantiate(HealthPotionPrefab);
        HealthPotion.transform.position = this.gameObject.transform.position;

        HealthPotion.GetComponent<HealthPotion>().PotionMove();
    }
}
