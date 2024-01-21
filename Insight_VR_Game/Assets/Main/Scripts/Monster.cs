using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform finishPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        finishPoint = GameObject.Find("Finish Point Box").transform;
        agent.SetDestination(finishPoint.position);
    }
}
