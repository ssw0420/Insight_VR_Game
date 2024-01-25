using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    
    public Transform finishPoint;

    //Monster Fade Out
    Renderer render;
    Color rendererColor;
    [SerializeField]
    float wfs;

    [SerializeField]
    int health = 5;
    bool isHit = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        render = gameObject.GetComponentInChildren<Renderer>();
        rendererColor = render.materials[0].color;
    }

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        int randZ = Random.Range(-2, 2);
        agent.SetDestination(finishPoint.position + new Vector3(0, 0, randZ));

        StartCoroutine(TestCode());
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

    void OnAttack()
    {
        Debug.Log("¸ñÀûÁö µµÂø");
        anim.SetBool("isAttack", true);
        agent.speed = 0f;
    }

    void OnHit()
    {
        health -= 1;
        if (health <= 0)
        {
            Die();
            return;
        }
            
        anim.SetBool("isHit", true);
        isHit = true;

        StartCoroutine("HitOut");
    }

    IEnumerator HitOut()
    {
        float saveSpeed = agent.speed;
        agent.speed = 0;

        float curAnimationTime = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(curAnimationTime);

        anim.SetBool("isHit", false);
        agent.speed = saveSpeed;
    }

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

    IEnumerator TestCode()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            OnHit();
        }
    }
}
