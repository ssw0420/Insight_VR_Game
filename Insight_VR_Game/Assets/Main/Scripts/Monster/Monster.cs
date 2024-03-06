using MagicPigGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState { 
    Idle,
    Walk,
    SkillHit,
    Attack,
    Hit,
    Die,
    Win,
}

public class Monster : MonoBehaviour
{
    MonsterState m_State;
    public static Monster instance;
    //움직임 및 애니메이션
    protected Animator anim;
    protected NavMeshAgent agent;
    public Transform finishPoint;

    //Fade Out 관련 변수
    protected Renderer render;
    [SerializeField] float fadeDelay;

    [SerializeField]
    public int health = 5;

    //Hit 관련 변수
    [Header("Hit variable")]
    List<AudioClip> hitAudios;
    public Material hitMaterial;
    [SerializeField]float damage;
    [SerializeField]float hitDelay;
    [SerializeField] protected float curHitAnimationTime;
    [SerializeField] protected float curAttackAnimationTime;

    //몬스터 플레이어 회전
    protected Camera player;

    //몬스터 소리
    protected AudioSource monsterAudio;

    float saveSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        monsterAudio = GetComponent<AudioSource>();
        player = Camera.main;
        if (instance == null)
        {
            instance = this;
        }
    }

    protected virtual void Start()
    {
        m_State = MonsterState.Walk;
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));
        saveSpeed = agent.speed;
        hitMaterial = MonsterManager.Instance.GetHitMaterial();
    }

    public void SetMonsterDestination()
    {
        if (m_State != MonsterState.SkillHit)
            return;

        m_State = MonsterState.Walk;
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));
        
    }

    public void SetAudio(List<AudioClip> hitAudio)
    {
        hitAudios = hitAudio.ToList();
    }

    private void Update()
    {
        switch (m_State)
        {
            case MonsterState.Walk:
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
                {
                    StartCoroutine(OnAttack());
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (m_State)
        {
            case MonsterState.Attack:
            case MonsterState.Idle:
                MonsterVision();
                break;
        }
    }

    protected void MonsterVision()
    {
        Vector3 lookDir = (player.transform.position - transform.position).normalized;

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * 9f);
    }

    //공격 부분
    IEnumerator OnAttack()
    {
        agent.speed = 0f;
        anim.SetBool("isWalk", false);
        m_State = MonsterState.Attack;

        while (true)
        {
            if (m_State == MonsterState.Win || m_State == MonsterState.SkillHit)
                break;

            Debug.Log("공격");
            anim.SetTrigger("isAttack");    
            m_State = MonsterState.Attack;

            yield return new WaitForSeconds(curAttackAnimationTime - 0.31f);

            m_State = MonsterState.Idle;
            Player.Instance.PlayerHit();

            //PlayerStats.Instance.TakeDamage(damage);
            if(PlayerController.instance.HealthState == false)
                ProgressBarInspectorTest.instance.progress -= damage / 10.0f;
            else if(PlayerController.instance.HealthState == true)
                ProgressBarInspectorTest.instance.progress -= damage / 10.0f / 2.0f;

            yield return new WaitForSeconds(hitDelay);
        }
    }

    //맞는 부분
    public virtual void OnHit(int damage, string weaponType)
    {

        if (m_State == MonsterState.Die)
            return;

        switch (weaponType)
        {
            case "Arrow":
                monsterAudio.clip = hitAudios[0];
                break;
            case "Ice":
                monsterAudio.clip = hitAudios[1];
                break;
        }

        health -= damage;
        if (health <= 0)
        {
            m_State = MonsterState.Die;
            Die();
            return;
        }
            
        anim.SetBool("isHit", true);
        monsterAudio.time = 0f;
        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        monsterAudio.Play();
        LayerMask saveLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Hit Monster");

        float saveSpeed = agent.speed;
        MonsterState saveState = m_State;
        agent.speed = 0;

        Material saveMaterial = render.materials[0];
        render.material = hitMaterial;

        m_State = MonsterState.Hit;
        yield return new WaitForSeconds(curHitAnimationTime);

        monsterAudio.Stop();
        gameObject.layer = saveLayer;

        agent.speed = saveSpeed;

        m_State = saveState;
        render.material = saveMaterial;

        anim.SetBool("isHit", false);
    }

    //죽는 부분
    protected virtual void Die()
    {
        StopAllCoroutines();
        m_State = MonsterState.Die;
        gameObject.layer = LayerMask.NameToLayer("Default");
        anim.SetTrigger("isDie");
        agent.enabled = false;

        StartCoroutine("MonsterHitEffect");
        StartCoroutine(MonsterFadeOut());
    }

    IEnumerator MonsterHitEffect()
    {
        monsterAudio.Play();
        Material saveMaterial = render.materials[0];
        render.material = hitMaterial;

        yield return new WaitForSeconds(0.677f);

        render.material = saveMaterial;
        monsterAudio.Stop();
    }

    IEnumerator MonsterFadeOut()
    {
        yield return new WaitForSeconds(fadeDelay);

        Color color = render.materials[0].color;
        float time = 0f;
        float fadeTime = 1f;

        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            render.materials[0].color = color;

            yield return new WaitForSeconds(0.001f);
        }

        MonsterManager.Instance.DeleteLiveMonsterList(this.gameObject);
        Destroy(gameObject);
    }

    public virtual void HitBlackHole(Vector3 hitPos)
    {
        m_State = MonsterState.SkillHit;

        anim.SetBool("isWalk", true);
        agent.speed = 2f;

        agent.SetDestination(hitPos);
        MonsterVision();
    }

    public void Win()
    {
        m_State = MonsterState.Win;
        StopAllCoroutines();
        agent.speed = 0;

        anim.SetTrigger("GameLose");
    }
}