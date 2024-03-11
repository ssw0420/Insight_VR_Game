using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MagicPigGames;
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
    public BossState b_State;

    List<GameObject> BossEyeHit;
    ParticleSystem bossSkillEffect;
    AudioSource skillAudio;
    List<AudioClip> hitAudios;
    List<Transform> bossFinishPoint;
    Transform bossSkillPos;
    int checkPoint = 0;
    int savePoint = -1;
    int maxHealth = 100;
    int skillCount = 0;
    bool isHit = false;

    protected override void Start()
    {
        health = maxHealth;
        finishPoint = GameObject.Find("Boss Attack Pos").transform;
        bossSkillPos = GameObject.Find("Boss Skill Pos").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        bossSkillEffect = GetComponentInChildren<ParticleSystem>();
        render = GetComponentsInChildren<Renderer>()[3];
        skillAudio = GetComponentInChildren<AudioSource>();
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
        int pointNum = 0;
        while (savePoint == pointNum)
            pointNum = UnityEngine.Random.Range(0, 4);
        savePoint = pointNum;

        agent.SetDestination(bossFinishPoint[pointNum].position);

        yield return null;
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
        if (PlayerController.instance.HealthState == false)
            ProgressBarInspectorTest.instance.progress -= 0.4f;
        else if (PlayerController.instance.HealthState == true)
            ProgressBarInspectorTest.instance.progress -= 0.2f;
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
        skillAudio.clip = hitAudios[1];
        skillAudio.Play();

        if (PlayerController.instance.HealthState == false)
            ProgressBarInspectorTest.instance.progress -= 0.3f;
        else if (PlayerController.instance.HealthState == true)
            ProgressBarInspectorTest.instance.progress -= 0.15f;

        yield return new WaitForSeconds(2f);
        anim.SetBool("isAttack", false);

        BossMove();
    }

    //보스 맞는 부분
    public override void OnHit(int damage, string weaponType)
    {
        if (isHit || b_State == BossState.Die)
            return;

        health -= damage;
        BossHealthBar.Instance.HealthUIUpdate(maxHealth, health);
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
            

        StartCoroutine(HitOut());
    }

    IEnumerator HitOut()
    {
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        skillAudio.clip = hitAudios[0];
        skillAudio.Play();
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
        BossHealthBar.Instance.HealthUIUpdate(maxHealth, health);
        if (health <= 0)
        {
            b_State = BossState.Die;
            Die();
        }
           
        if (b_State == BossState.Skill)
        {
            StopAllCoroutines();
            StartCoroutine(StopSkill());
            return;
        }

        StartCoroutine(CriticalHitOut());
    }

    IEnumerator CriticalHitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        anim.SetTrigger("isHit");

        yield return new WaitForSeconds(0.733f);

        isHit = false;
        render.material = saveMeterial;
        agent.speed = saveSpeed;
    }

    IEnumerator StopSkill()
    {
        anim.SetTrigger("isHit");
        bossSkillEffect.Stop();
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;

        yield return new WaitForSeconds(0.01f);
        anim.SetBool("isAttack", false);

        yield return new WaitForSeconds(0.733f);

        render.material = saveMeterial;
        isHit = false;
        BossMove();
    }

    public override void HitBlackHole(Vector3 hitPos)
    {

    }
}