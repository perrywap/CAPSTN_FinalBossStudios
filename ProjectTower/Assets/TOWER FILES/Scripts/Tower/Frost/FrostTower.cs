using UnityEngine;

public class FrostTower : Tower
{
    [Header("Frost Settings")]
    [SerializeField] private GameObject frostballPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Animation")]
    [SerializeField] private FrostTowerAnimator animator;

    private void Start()
    {
        if (animator != null)
        {
            animator.OnCastFrameReached += () =>
            {
                Unit target = GetNearestTarget();
                if (target != null)
                {
                    Attack(target);
                    fireCooldown = 1f / fireRate;
                }
            };
        }
    }

    protected override void Update()
    {
        base.Update();

        Unit target = GetNearestTarget();

        if (target != null)
        {
            animator.PlayAttack();
        }
        else
        {
            animator.PlayIdle();
        }
    }

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
