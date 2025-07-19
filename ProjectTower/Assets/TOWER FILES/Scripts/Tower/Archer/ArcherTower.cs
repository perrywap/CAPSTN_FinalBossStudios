using UnityEngine;

public class ArcherTower : Tower
{
    [Header("Archer Settings")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Visual References")]
    [SerializeField] private ArcherIdleAnimator idleAnimator;
    [SerializeField] private GameObject bowObject;

    protected override void Update()
    {
        base.Update();

        Unit target = GetNearestTarget();

        if (target != null)
        {
            idleAnimator.StopIdle();
            if (bowObject != null) bowObject.SetActive(true);
        }
        else
        {
            idleAnimator.StartIdle();
            if (bowObject != null) bowObject.SetActive(false);
        }
    }

    protected override void Attack(Unit target)
    {
        if (arrowPrefab != null && firePoint != null && target != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = arrow.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.SetTarget(target, damage);
            }
        }
    }
}
