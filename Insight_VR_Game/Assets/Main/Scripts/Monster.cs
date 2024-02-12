using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //움직임 및 애니메이션
    protected Animator anim;
    protected NavMeshAgent agent;
    public Transform finishPoint;

    //Fade Out 관련 변수
    protected Renderer render;

    [SerializeField]
    public int health = 5;

    //Hit 관련 변수
    public Material hitMaterial;
    protected float curHitAnimationTime;

    //몬스터 플레이어 회전
    protected Camera camera;

    //몬스터 소리
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

        //Hit 애니메이션 시간 구하기
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == "GetHit")
                curHitAnimationTime = ac.animationClips[i].length;
    }

    public void SetAudio(AudioClip hitAudio)
    {
        monsterAudio.clip = hitAudio;
    }

    private void Update()
    {
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            OnAttack();
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

    //공격 부분
    void OnAttack()
    {
        anim.SetBool("isAttack", true);
        agent.speed = 0f;
    }

    //맞는 부분
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

    //죽는 부분
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
