using UnityEngine;

public class MortarTower : Tower
{
    [Header("Mortar Settings")]
    [SerializeField] private GameObject mortarPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float minRange = 3f;
    [SerializeField] private float maxRange = 8f;

    protected override void Attack(Unit target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (mortarPrefab != null && firePoint != null && target != null && distance >= minRange && distance <= maxRange)
        {
            GameObject mortar = Instantiate(mortarPrefab, firePoint.position, Quaternion.identity);
            MortarProjectile projectile = mortar.GetComponent<MortarProjectile>();

            if (projectile != null)
            {
                projectile.SetTarget(target, damage);
            }
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}