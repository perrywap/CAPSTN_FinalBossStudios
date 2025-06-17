using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindMovement : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform mainTower;
    private Transform targetTower;
    private float lastAttackTime;

    private Unit unit;
    private NavMeshAgent agent;

    private bool isAttackingTower = false;

    private float AttackRange => unit.AttackRange; 

    private void Start()
    {
        unit = GetComponent<Unit>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        mainTower = GameObject.FindGameObjectWithTag("MainTower").transform;
        agent.SetDestination(mainTower.position);
    }

    private void Update()
    {
        if (isAttackingTower)
        {
            if (targetTower == null)
            {
                isAttackingTower = false;
                agent.SetDestination(mainTower.position);
                return;
            }

            float distance = Vector3.Distance(transform.position, targetTower.position);

            if (distance > AttackRange)
            {
                agent.SetDestination(targetTower.position);
            }
            else
            {
                agent.ResetPath();

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackTower(targetTower.gameObject);
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            DetectNearestTower();

            if (targetTower != null)
            {
                isAttackingTower = true;
                agent.SetDestination(targetTower.position);
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= 0.1f)
                {
                    unit.OnPathComplete();
                }
            }
        }
    }

    private void DetectNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        float closestDistance = detectionRadius;
        Transform closestTower = null;

        foreach (GameObject tower in towers)
        {
            if (tower == null) continue;

            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestTower = tower.transform;
            }
        }

        if (closestTower != null)
        {
            targetTower = closestTower;
        }
    }

    private void AttackTower(GameObject tower)
    {
        TowerUnit towerUnit = tower.GetComponent<TowerUnit>();
        if (towerUnit != null)
        {
            towerUnit.TakeDamage(unit.AttackDamage);
            Debug.Log($"{gameObject.name} attacked {tower.name} for {unit.AttackDamage} damage. Tower HP: {towerUnit.CurrentHealth}");

            if (towerUnit.CurrentHealth <= 0)
            {
                Debug.Log($"{tower.name} has been destroyed!");
                Destroy(tower);
                targetTower = null;
            }
        }
        else
        {
            targetTower = null;
        }
    }
}
