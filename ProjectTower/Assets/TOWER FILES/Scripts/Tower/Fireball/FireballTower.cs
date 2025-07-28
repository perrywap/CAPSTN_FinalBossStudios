using UnityEngine;

public class FireballTower : Tower
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Animation")]
    [SerializeField] private FireballTowerAnimator animator;

    private void Start()
    {
        if (animator != null)
        {
            animator.OnGlowFrameReached += () =>
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

    public override void Attack(Unit target)
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