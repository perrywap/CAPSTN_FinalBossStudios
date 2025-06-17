using UnityEngine;

public class MortarTower : Tower
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject mortarPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float predictionFactor = 0.5f;

    protected override void Attack(Unit target)
    {
        if (mortarPrefab != null && firePoint != null && target != null)
        {
            Vector3 predictedPosition = PredictFuturePosition(target);

            GameObject mortar = Instantiate(mortarPrefab, firePoint.position, Quaternion.identity);
            MortarProjectile projectile = mortar.GetComponent<MortarProjectile>();

            if (projectile != null)
            {
                projectile.SetTarget(predictedPosition, damage);
            }
        }
    }

    private Vector3 PredictFuturePosition(Unit target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector3 velocity = rb != null ? (Vector3)rb.velocity : Vector3.zero;
        return target.transform.position + velocity * predictionFactor;
    }
}
