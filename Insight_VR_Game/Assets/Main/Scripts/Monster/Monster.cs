using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
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
    protected float curHitAnimationTime;
    protected float curAttackAnimationTime;
    bool isAttack = false;

    //���� �÷��̾� ȸ��
    protected Camera camera;

    //���� �Ҹ�
    protected AudioSource monsterAudio;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        monsterAudio = GetComponent<AudioSource>();
        camera = Camera.main;
    }

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));

        //Hit �ִϸ��̼� �ð� ���ϱ�
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "GetHit")
                curHitAnimationTime = ac.animationClips[i].length;
            else if (ac.animationClips[i].name == "Attack01")
                curAttackAnimationTime = ac.animationClips[i].length;

        }
            
    }

    public void SetAudio(AudioClip hitAudio)
    {
        monsterAudio.clip = hitAudio;
    }

    private void Update()
    {
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            if (isAttack)
                return;

            StartCoroutine(OnAttack());
        }  
    }

    void FixedUpdate()
    {
        if (anim.GetBool("isAttack"))
        {
            Vector3 lookDir = (camera.transform.position - transform.position).normalized;

            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.LookRotation(lookDir);

            transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * 9f);
        }
    }

    //���� �κ�
    IEnumerator OnAttack()
    {
        agent.speed = 0f;
        isAttack = true;
        while (true)
        {
            anim.SetBool("isAttack", true);
            yield return new WaitForSeconds(curAttackAnimationTime);
            anim.SetBool("isAttack", false);
            PlayerStats.Instance.TakeDamage(damage);
            yield return new WaitForSeconds(hitDelay);
        }
    }

    //�´� �κ�
    public virtual void OnHit(int damage)
    {
        if (anim.GetBool("isHit"))
            return;

        health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }
            
        anim.SetBool("isHit", true);
        monsterAudio.time = 0f;
        monsterAudio.Play();
        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;
        Material saveMaterial = render.materials[0];
        render.material = hitMaterial;

        yield return new WaitForSeconds(curHitAnimationTime);

        render.material = saveMaterial;
        anim.SetBool("isHit", false);
        monsterAudio.Stop();
        agent.speed = saveSpeed;
    }

    //�״� �κ�
    protected virtual void Die()
    {
        anim.SetTrigger("isDie");
        agent.enabled = false;

        StartCoroutine(MonsterFadeOut());
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
}
