using MagicPigGames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState { 
    Idle,
    Walk,
    Attack,
    Hit,
    Die,
    Win,
}

public class Monster : MonoBehaviour
{
    MonsterState m_State;
    public static Monster instance;
    //������ �� �ִϸ��̼�
    protected Animator anim;
    protected NavMeshAgent agent;
    public Transform finishPoint;

    //Fade Out ���� ����
    protected Renderer render;

    [SerializeField]
    public int health = 5;

    //Hit ���� ����
    [Header("Hit variable")]
    public Material hitMaterial;
    [SerializeField]float damage;
    [SerializeField]float hitDelay;
    [SerializeField] protected float curHitAnimationTime;
    [SerializeField] protected float curAttackAnimationTime;

    //���� �÷��̾� ȸ��
    protected Camera player;

    //���� �Ҹ�
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

    //Get AnimationTime;
    protected float GetAnimationClipLenght()
    {
        float time = 0;
        string name;
        switch (m_State)
        {
            case MonsterState.Hit:
                name = "GetHit";
                break;
            case MonsterState.Attack:
                name = "Attack01";
                break;
            default:
                name = "None";
                break;
        }

        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
                time = ac.animationClips[i].length;
        }

        return time;
    }

    public void SetAudio(AudioClip hitAudio)
    {
        monsterAudio.clip = hitAudio;
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

    //���� �κ�
    IEnumerator OnAttack()
    {
        agent.speed = 0f;
        anim.SetBool("isWalk", false);
        m_State = MonsterState.Attack;

        while (true)
        {
            if (m_State == MonsterState.Win)
                break;

            Debug.Log("����");
            anim.SetTrigger("isAttack");    
            m_State = MonsterState.Attack;

            curAttackAnimationTime = GetAnimationClipLenght();
            yield return new WaitForSeconds(curAttackAnimationTime);

            m_State = MonsterState.Idle;

            //PlayerStats.Instance.TakeDamage(damage);
            if(PlayerController.instance.HealthState == false)
                ProgressBarInspectorTest.instance.progress -= damage;
            else if(PlayerController.instance.HealthState == true)
                ProgressBarInspectorTest.instance.progress -= damage / 2.0f;

            yield return new WaitForSeconds(hitDelay);
        }
    }

    //�´� �κ�
    public virtual void OnHit(int damage)
    {
        if (m_State == MonsterState.Hit)
            return;
        if (m_State == MonsterState.Die)
            return;

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
        float saveSpeed = agent.speed;
        MonsterState saveState = m_State;
        agent.speed = 0;
        Material saveMaterial = render.materials[0];
        render.material = hitMaterial;

        m_State = MonsterState.Hit;
        yield return new WaitForSeconds(curHitAnimationTime);

        monsterAudio.Stop();
        agent.speed = saveSpeed;
        m_State = saveState;
        render.material = saveMaterial;
        anim.SetBool("isHit", false);
    }

    //�״� �κ�
    protected virtual void Die()
    {
        //StopCoroutineall
        m_State = MonsterState.Die;
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
        yield return new WaitForSeconds(3f);

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

        Destroy(gameObject);
        MonsterManager.Instance.DeleteLiveMonsterList(this.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("���� ��ų ��Ʈ ����");
        OnHit(1);
    }

    public void Win()
    {
        m_State = MonsterState.Win;
        StopAllCoroutines();
        agent.speed = 0;

        anim.SetTrigger("GameLose");
    }
}