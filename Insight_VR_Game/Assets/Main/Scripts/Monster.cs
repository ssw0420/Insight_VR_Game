using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    Renderer render;
    Color rendererColor;
    public Transform finishPoint;
    public float wfs;

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
        Debug.Log(agent.destination);

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
        yield return new WaitForSeconds(5f);
        Die();
    }
}
