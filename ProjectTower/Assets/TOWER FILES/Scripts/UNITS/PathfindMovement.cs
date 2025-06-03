using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Unit unit;
    private NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainTower").transform;
        
        unit = this.GetComponent<Unit>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (agent.remainingDistance == 0)
        {
            unit.OnPathComplete();
        }
    }
}