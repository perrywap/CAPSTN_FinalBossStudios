using UnityEngine;

public class FireballTower : Tower
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;

    protected override void Attack(Unit target)
    {
        if (fireballPrefab != null && firePoint != null && target != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            FireballProjectile projectile = fireball.GetComponent<FireballProjectile>();

            if (projectile != null)
            {
                projectile.SetTarget(target, damage);
            }
        }
    }
}
