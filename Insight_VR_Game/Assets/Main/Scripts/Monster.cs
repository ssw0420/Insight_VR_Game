using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    public Transform finishPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        agent.SetDestination(finishPoint.position);
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            OnAttack();
    }

    void OnAttack()
    {
        anim.SetBool("isAttack", true);
        agent.speed = 0f;
    }

    void Die()
    {
        anim.SetTrigger("isDie");
        agent.enabled = false;
    }
}
