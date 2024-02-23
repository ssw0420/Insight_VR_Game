using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Walk,
    Attack,
    Skill,
    Hit,
    Die,
}

public class Boss : Monster
{
    BossState b_State;

    ParticleSystem bossSkillEffect;
    List<Transform> bossFinishPoint;
    Transform bossSkillPos;
    int checkPoint = 0;
    int maxHealth = 100;
    int skillCount = 0;
    bool isHit = false;

    private void Start()
    {
        health = maxHealth;
        finishPoint = GameObject.Find("Boss Attack Pos").transform;
        bossSkillPos = GameObject.Find("Boss Skill Pos").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        bossSkillEffect = GetComponentInChildren<ParticleSystem>();
        b_State = BossState.Walk;
        BossMove();
    }

    private void Update()
    {
        switch (b_State)
        {
            case BossState.Walk:
                if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.1f)
                {
                    if (checkPoint >= 3)
                        StartCoroutine(PlayerAttack());
                    else
                        BossMove();
                }
                break;
            case BossState.Attack:
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.1f)
                {
                    if (anim.GetBool("isAttack"))
                        return;

                    StartCoroutine("OnAttack");
                }
                break;
            case BossState.Skill:
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.1f)
                {
                    StartCoroutine(OnSkill());
                }
                break;
        }

        if (health <= (maxHealth * ((3 - skillCount) * 25) / 100))
        {
            skillCount++;
            b_State = BossState.Skill;
            GoToSkill();
        }
    }

    //보스 움직임
    void BossMove()
    {
        b_State = BossState.Walk;
        checkPoint++;
        Debug.Log(checkPoint + " 보스 이동");
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        int pointNum = UnityEngine.Random.Range(0, 4);
        agent.SetDestination(bossFinishPoint[pointNum].position);

        yield return new WaitForSeconds(0.5f);
    }

    private void FixedUpdate()
    {
        if (anim.GetBool("isAttack"))
            MonsterVision();
    }

    //플레이어 공격
    IEnumerator PlayerAttack()
    {
        yield return null;
        b_State = BossState.Attack;
        checkPoint = 0;
        agent.SetDestination(finishPoint.position);
        Debug.Log("공격 시전 이동");
    }

    IEnumerator OnAttack()
    {
        int attackNum = UnityEngine.Random.Range(1, 2);
        anim.SetInteger("AttackNum", attackNum);
        anim.SetBool("isAttack", true);
        Debug.Log("공격 시전");
        yield return new WaitForSeconds(0.733f);
        PlayerStats.Instance.TakeDamage(1);
        anim.SetBool("isAttack", false);

        if (b_State == BossState.Skill)
            GoToSkill();
        else
            BossMove();
    }

    void GoToSkill()
    {
        if (b_State == BossState.Attack)
            return;

        agent.SetDestination(bossSkillPos.position);
        Debug.Log("스킬 시전하러 이동");
    }

    IEnumerator OnSkill()
    {
        b_State = BossState.Skill;
        anim.SetBool("isAttack", true);
        anim.SetInteger("AttackNum", 3);
        Debug.Log("스킬 시전");

        yield return new WaitForSeconds(3f);
        bossSkillEffect.Play();

        yield return new WaitForSeconds(2f);
        anim.SetBool("isAttack", false);

        BossMove();
    }

    //보스 맞는 부분
    public override void OnHit(int damage)
    {
        if (isHit || b_State == BossState.Die)
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
        if (isHit || b_State == BossState.Die)
            return;

        isHit = true;
        health -= damage;
        if (health <= 0)
        {
            b_State = BossState.Die;
            Die();
        }
            

        if (b_State == BossState.Skill)
        {
            StopAllCoroutines();
            anim.SetBool("isHit", true);
            anim.SetBool("isAttack", false);
            bossSkillEffect.Stop();
            StartCoroutine(CriticalHitOut());
            BossMove();
        }

        StartCoroutine(CriticalHitOut());
    }

    IEnumerator CriticalHitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        anim.SetBool("isHit", true);

        yield return new WaitForSeconds(1f);

        isHit = false;
        render.material = saveMeterial;
        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
    }
}