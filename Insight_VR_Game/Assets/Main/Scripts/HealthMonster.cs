using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonster : Monster
{
    GameObject HealthPotionPrefab;

    private void Start()
    {
        HealthPotionPrefab = MonsterManager.Instance.GetHealthPotionPrefab();
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));

        //Hit �ִϸ��̼� �ð� ���ϱ�
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == "GetHit")
                curHitAnimationTime = ac.animationClips[i].length;
    }

    protected override void Die()
    {
        base.Die();
        GameObject HealthPotion = Instantiate(HealthPotionPrefab);
        HealthPotion.transform.position = this.gameObject.transform.position + Vector3.up;

        HealthPotion.GetComponent<HealthPotion>().PotionMove();
    }
}
