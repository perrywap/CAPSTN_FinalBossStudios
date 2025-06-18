using System.Collections;
using UnityEngine;

public class FrostProjectile : Projectile
{
    [Header("AOE Settings")]
    [SerializeField] private float explosionRadius = 1.5f;

    [Header("Slow Settings")]
    [SerializeField] private float slowAmount = 1f;
    [SerializeField] private float slowDuration = 3f;

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
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            Unit enemy = hit.GetComponent<Unit>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                StartCoroutine(ApplyTemporarySlow(enemy));
            }
        }

        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
        Destroy(gameObject, slowDuration + 0.1f);
    }

    private IEnumerator ApplyTemporarySlow(Unit enemy)
    {
        if (enemy == null) yield break;

        float actualSlow = Mathf.Min(slowAmount, enemy.Speed);
        enemy.Speed -= actualSlow;

        yield return new WaitForSeconds(slowDuration);

        if (enemy != null)
        {
            enemy.Speed += actualSlow;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
