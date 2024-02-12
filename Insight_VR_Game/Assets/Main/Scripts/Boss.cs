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

    private void Start()
    {
        health = maxHealth;
        finishPoint = GameObject.Find("Finish Point Box").transform;
        bossSkillPos = GameObject.Find("BossSkillPos").transform;
        bossFinishPoint = MonsterManager.Instance.GetBossPointList().ToList();
        BossMove();
    }

    //보스 움직임
    void BossMove()
    {
        savePoint++;
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
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.1f)
        {
            if (isAttack)
                StartCoroutine(OnAttack());
            else if (isSkill)
            {
                StartCoroutine(OnSkill());
            }
            else
            {
                if (savePoint >= 2)
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

        if(health <= (maxHealth * ((3-index) * 25) / 100))
        {
            index++;
            isSkill = true;
            PlayerSkill();
        }
        //if(health <= (maxHealth * 75 / 100))
        //{
        //    if (skillHealth[0])
        //        return;

        //    isSkill = true;
        //    skillHealth[0] = true;
        //    PlayerSkill();
        //    Debug.Log("체력 75% 남음");
        //}
        //else if (health <= (maxHealth * 50 / 100))
        //{
        //    if (skillHealth[1])
        //        return;

        //    isSkill = true;
        //    skillHealth[1] = true;
        //    PlayerSkill();
        //    Debug.Log("체력 50% 남음");
        //}
        //else if (health <= (maxHealth * 25 / 100))
        //{
        //    if (skillHealth[2])
        //        return;

        //    isSkill = true;
        //    skillHealth[2] = true;
        //    PlayerSkill();
        //    Debug.Log("체력 25% 남음");
        //}
    }

    //플레이어 공격
    IEnumerator PlayerAttack()
    {
        yield return null;
        agent.SetDestination(finishPoint.position);
        isAttack = true;
        attackNum = Random.Range(1, 2);
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
            PlayerSkill();
        else
            BossMove();
    }

    void PlayerSkill()
    {
        if (isAttack)
            return;
        agent.SetDestination(bossSkillPos.position);
        Debug.Log("스킬 시전하러 이동");
    }

    //보스 맞는 부분
    IEnumerator OnSkill()
    {
        anim.SetBool("isAttack", true);
        anim.SetInteger("AttackNum", 3);
        Debug.Log("스킬 시전");
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

        if (isSkill)
            StopAllCoroutines();

        StartCoroutine(CriticalHitOut());
    }

    IEnumerator CriticalHitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMeterial = render.materials[0];
        render.material = hitMaterial;
        isHit = true;

        yield return new WaitForSeconds(1f);

        render.material = saveMeterial;
        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
        isHit = false;
    }
}
