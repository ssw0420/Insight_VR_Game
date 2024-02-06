using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Monster
{
    int deathCount = 5;

    List<Transform> bossFinishPoint;
    int pointNum;
    int attackNum;
    bool isAttack = false;

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        BossMove();
        StartCoroutine(PlayerAttack());
    }

    //보스 움직임
    void BossMove()
    {
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        yield return null;
        pointNum = Random.Range(0, 4);

        agent.SetDestination(bossFinishPoint[pointNum].position);
    }

    private void Update()
    {
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            if (isAttack)
                StartCoroutine(OnAttack());
            else
                BossMove();
        }
    }

    //플레이어 공격
    IEnumerator PlayerAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            isAttack = true;
            attackNum = Random.Range(1, 3);
            agent.SetDestination(finishPoint.position);
        }
        
    }

    IEnumerator OnAttack()
    {
        anim.SetBool("isAttack", isAttack);
        anim.SetInteger("AttackNum", attackNum);

        yield return new WaitForSeconds(0.733f);
        isAttack = false;
        anim.SetBool("isAttack", isAttack);
    }

    //보스 맞는 부분
    public override void OnHit(GameObject hitPoint)
    {
        if (anim.GetBool("isHit"))
            return;

        deathCount++;
        if (deathCount >= 6)
            Die();

        anim.SetBool("isHit", true);
        Destroy(hitPoint);
        
        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;

        yield return new WaitForSeconds(1f);

        render.material = saveMeterial;
        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
    }
}
