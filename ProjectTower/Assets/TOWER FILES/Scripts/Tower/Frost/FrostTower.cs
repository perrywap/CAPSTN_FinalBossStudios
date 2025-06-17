using UnityEngine;

public class FrostTower : Tower
{
    [Header("Frost Settings")]
    [SerializeField] private GameObject frostballPrefab;
    [SerializeField] private Transform firePoint;

    protected override void Attack(Unit target)
    {
        if (frostballPrefab != null && firePoint != null && target != null)
        {
            GameObject frostball = Instantiate(frostballPrefab, firePoint.position, Quaternion.identity);
            FrostProjectile projectile = frostball.GetComponent<FrostProjectile>();

            if (projectile != null)
            {
                projectile.SetTarget(target, damage);
            }
        }
    }
}
