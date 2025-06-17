using UnityEngine;

public class ArcherTower : Tower
{
    [Header("Archer Settings")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

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
