using UnityEngine;

public class ArcherProjectile : Projectile
{
    private Unit target;

    public override void SetTarget(Unit newTarget, float dmg)
    {
        base.SetTarget(newTarget, dmg);
        target = newTarget;
    }

    private void Update()
    {
        if (!initialized || target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < 0.2f)
        {
            target.TakeDamage(damage);
            target.Die();
            Destroy(gameObject);
        }
    }
}
