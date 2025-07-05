using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] protected float range = 3f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float fireRate = 1f;

    protected float fireCooldown = 0f;
    protected List<Unit> targetsInRange = new List<Unit>();

    protected virtual void Update()
    {
        fireCooldown -= Time.deltaTime;
        RemoveNullTargets();

        if (targetsInRange.Count > 0 && fireCooldown <= 0f)
        {
            Unit target = GetNearestTarget();
            if (target != null)
            {
                Attack(target);
                fireCooldown = 1f / fireRate;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null && !targetsInRange.Contains(unit))
        {
            targetsInRange.Add(unit);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            targetsInRange.Remove(unit);
        }
    }

    protected void RemoveNullTargets()
    {
        targetsInRange.RemoveAll(u => u == null);
    }

    protected Unit GetNearestTarget()
    {
        Unit closest = null;
        float shortestDist = Mathf.Infinity;

        foreach (Unit u in targetsInRange)
        {
            float dist = Vector2.Distance(transform.position, u.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closest = u;
            }
        }

        return closest;
    }

    protected virtual void Attack(Unit target)
    {

    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
