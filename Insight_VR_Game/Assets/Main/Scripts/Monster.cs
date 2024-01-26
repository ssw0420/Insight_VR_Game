using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //테스트
    GameManager manager;

    Animator anim;
    NavMeshAgent agent;
    
    public Transform finishPoint;

    //Fade Out 관련 변수
    Renderer render;
    Color rendererColor;
    [SerializeField]
    float wfs;

    [SerializeField]
    public int health = 5;

    //Hit 관련 변수
    float curHitAnimationTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        render = gameObject.GetComponentInChildren<Renderer>();
        rendererColor = render.materials[0].color;

        //테스트
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        //테스트 코드
        //StartCoroutine(TestCode());

        //테스트
        manager.InputList(this);
    }

    private void Update()
    {
        //if (agent.remainingDistance < agent.stoppingDistance)
        //    OnAttack();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            OnAttack();
        }
    }

    //공격 부분
    void OnAttack()
    {
        Debug.Log("목적지 도착");
        anim.SetBool("isAttack", true);
        agent.speed = 0f;
    }

    //맞는 부분
    public void OnHit()
    {
        if (anim.GetBool("isHit"))
            return;

        health -= 1;
        if (health <= 0)
        {
            Die();
            return;
        }
            
        anim.SetBool("isHit", true);

        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;

        yield return new WaitForSeconds(curHitAnimationTime);

        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
    }

    //죽는 부분
    void Die()
    {
        anim.SetTrigger("isDie");
        agent.enabled = false;

        StartCoroutine(MonsterFadeOut());
    }

    IEnumerator MonsterFadeOut()
    {
        yield return new WaitForSeconds(3f);

        float alpha = 1;

        while (true)
        {
            alpha -= 0.001f;
            
            rendererColor.a = alpha;
            render.materials[0].color = rendererColor;

            if (alpha <= 0)
                break;

            yield return new WaitForSeconds(wfs);
        }

        Destroy(gameObject);
    }

    //테스트 코드
    IEnumerator TestCode()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            OnHit();
        }
    }
}
