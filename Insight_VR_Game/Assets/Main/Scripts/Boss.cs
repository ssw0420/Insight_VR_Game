using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Monster
{
    List<Transform> bossFinishPoint;
    Transform bossSkillPos;
    int pointNum;
    int attackNum;
    bool isAttack = false;
    int savePoint = 0;
    int maxHealth = 100;
    bool isSkill = false;

    private void Start()
    {
        health = maxHealth;
        finishPoint = GameObject.Find("Finish Point Box").transform;
        bossSkillPos = GameObject.Find("BossSkillPos").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        BossMove();
    }

    //���� ������
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
            else if (isSkill)
            {

            }
            else
            {
                savePoint++;

                if (savePoint >= 2)
                    StartCoroutine(PlayerAttack());
                else
                    BossMove();
            }
        }

        if(health <= (maxHealth * (75 / 100)))
        {
            if (isSkill)
                return;

            isSkill = true;
        }
    }

    //�÷��̾� ����
    IEnumerator PlayerAttack()
    {
        yield return null;
        agent.SetDestination(finishPoint.position);
        isAttack = true;
        attackNum = Random.Range(1, 3);
    }

    IEnumerator OnAttack()
    {
        anim.SetBool("isAttack", isAttack);
        anim.SetInteger("AttackNum", attackNum);

        yield return new WaitForSeconds(0.733f);
        isAttack = false;
        savePoint = 0;
        anim.SetBool("isAttack", isAttack);

        if (isSkill)
        {
            OnSkill();
        }
    }

    void OnSkill()
    {
        agent.SetDestination(bossSkillPos.position);
    }

    //���� �´� �κ�
    public void OnCriticalHit(int damage)
    {
        
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
