using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
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
    int index = 0;
    bool isHit = false;
    bool isCritical = false;

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
        savePoint++;
        Debug.Log(savePoint + " ���� �̵�");
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        pointNum = Random.Range(0, 4);
        agent.SetDestination(bossFinishPoint[pointNum].position);

        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        //Arrive at the Destination
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.1f)
        {
            //Attack Destination
            if (isAttack)
                StartCoroutine(OnAttack());
            //Skill Destination
            else if (isSkill)
            {
                StartCoroutine(OnSkill());
            }
            //Move Destination
            else
            {
                if (savePoint >= 3)
                {
                    if (isSkill)
                    {
                        savePoint = 0;
                        return;
                    }

                    StartCoroutine(PlayerAttack());
                }
                else
                {
                    BossMove();
                }
            }   
        }

        //Check Skill Health
        if(health <= (maxHealth * ((3-index) * 25) / 100))
        {
            index++;
            isSkill = true;
            PlayerSkill();
        }
    }

    //�÷��̾� ����
    IEnumerator PlayerAttack()
    {
        yield return null;
        agent.SetDestination(finishPoint.position);
        isAttack = true;
        attackNum = Random.Range(1, 2);
        Debug.Log("���� ���� �̵�");
    }

    IEnumerator OnAttack()
    {  
        anim.SetInteger("AttackNum", attackNum);
        anim.SetBool("isAttack", true);
        Debug.Log("���� ����");
        yield return new WaitForSeconds(0.733f);
        anim.SetBool("isAttack", false);
        isAttack = false;
        savePoint = 0;

        if (isSkill)
            PlayerSkill();
        else
            BossMove();
    }

    void PlayerSkill()
    {
        if (isAttack)
            return;
        agent.SetDestination(bossSkillPos.position);
        Debug.Log("��ų �����Ϸ� �̵�");
    }

    //���� �´� �κ�
    IEnumerator OnSkill()
    {
        anim.SetBool("isAttack", true);
        anim.SetInteger("AttackNum", 3);
        Debug.Log("��ų ����");

        yield return new WaitForSeconds(5f);

        anim.SetBool("isAttack", false);
        isSkill = false;

        BossMove();
    }

    public override void OnHit(int damage)
    {
        if (isHit)
            return;

        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();

        StartCoroutine(HitOut());
    }

    IEnumerator HitOut()
    {
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        isHit = true;

        yield return new WaitForSeconds(0.5f);

        isHit = false;
        render.material = saveMeterial;
    }

    public void OnCriticalHit(int damage)
    {
        if (isHit)
            return;

        health -= damage;
        if (health <= 0)
            Die();

        if (anim.GetBool("isAttack") && anim.GetInteger("AttackNum") == 3)
        {
            StopAllCoroutines();
            anim.SetBool("isHit", true);
            anim.SetBool("isAttack", false);
            isSkill = false;
            BossMove();
        }

        if (isCritical)
            HitOut();
        else
            StartCoroutine(CriticalHitOut());
    }

    IEnumerator CriticalHitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        anim.SetBool("isHit", true);
        isHit = true;
        //Stop Critical Hit
        StartCoroutine(StopCriticalHit());

        yield return new WaitForSeconds(1f);

        render.material = saveMeterial;
        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
        isHit = false;
    }

    IEnumerator StopCriticalHit()
    {
        isCritical = true;
        yield return new WaitForSeconds(3f);
        isCritical = false;
    }
}
