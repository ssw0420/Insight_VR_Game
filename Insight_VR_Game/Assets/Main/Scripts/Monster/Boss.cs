using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Monster
{
    ParticleSystem bossSkillEffect;
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
        finishPoint = GameObject.Find("Boss Attack Pos").transform;
        bossSkillPos = GameObject.Find("Boss Skill Pos").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        bossSkillEffect = GetComponentInChildren<ParticleSystem>();
        BossMove();
    }

    //보스 움직임
    void BossMove()
    {
        savePoint++;
        Debug.Log(savePoint + " 보스 이동");
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
            GoToSkill();
        }
    }

    private void FixedUpdate()
    {
        
    }

    //플레이어 공격
    IEnumerator PlayerAttack()
    {
        yield return null;
        agent.SetDestination(finishPoint.position);
        isAttack = true;
        attackNum = Random.Range(1, 2);
        PlayerStats.Instance.TakeDamage(1);
        Debug.Log("공격 시전 이동");
    }

    IEnumerator OnAttack()
    {  
        anim.SetInteger("AttackNum", attackNum);
        anim.SetBool("isAttack", true);
        Debug.Log("공격 시전");
        yield return new WaitForSeconds(0.733f);
        anim.SetBool("isAttack", false);
        isAttack = false;
        savePoint = 0;

        if (isSkill)
            GoToSkill();
        else
            BossMove();
    }

    void GoToSkill()
    {
        if (isAttack)
            return;
        agent.SetDestination(bossSkillPos.position);
        Debug.Log("스킬 시전하러 이동");
    }

    IEnumerator OnSkill()
    {
        anim.SetBool("isAttack", true);
        anim.SetInteger("AttackNum", 3);
        Debug.Log("스킬 시전");

        yield return new WaitForSeconds(3f);
        bossSkillEffect.Play();

        yield return new WaitForSeconds(2f);
        anim.SetBool("isAttack", false);
        isSkill = false;
        //bossSkillEffect.Stop();

        BossMove();
    }

    //보스 맞는 부분
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
            bossSkillEffect.Stop();
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