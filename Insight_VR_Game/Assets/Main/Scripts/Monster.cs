using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //�׽�Ʈ
    GameManager manager;

    Animator anim;
    NavMeshAgent agent;
    
    public Transform finishPoint;

    //Fade Out ���� ����
    Renderer render;
    Color rendererColor;
    [SerializeField]
    float wfs;

    [SerializeField]
    public int health = 5;

    //Hit ���� ����
    float curHitAnimationTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        render = gameObject.GetComponentInChildren<Renderer>();
        rendererColor = render.materials[0].color;

        //�׽�Ʈ
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));

        //Hit �ִϸ��̼� �ð� ���ϱ�
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == "GetHit")
                curHitAnimationTime = ac.animationClips[i].length;

        //�׽�Ʈ �ڵ�
        //StartCoroutine(TestCode());

        //�׽�Ʈ
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

    //���� �κ�
    void OnAttack()
    {
        Debug.Log("������ ����");
        anim.SetBool("isAttack", true);
        agent.speed = 0f;
    }

    //�´� �κ�
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

    //�״� �κ�
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

    //�׽�Ʈ �ڵ�
    IEnumerator TestCode()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            OnHit();
        }
    }
}
