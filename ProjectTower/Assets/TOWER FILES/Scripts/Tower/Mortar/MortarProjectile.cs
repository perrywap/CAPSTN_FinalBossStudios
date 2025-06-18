using UnityEngine;

public class MortarProjectile : Projectile
{
    [Header("AOE Settings")]
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float arcHeight = 2f;

    private Unit target;
    private Vector3 startPos;
    private float timeToTarget = 1f;
    private float elapsedTime = 0f;

    public override void SetTarget(Unit newTarget, float dmg)
    {
        base.SetTarget(newTarget, dmg);
        target = newTarget;
        startPos = transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / timeToTarget;

        if (t >= 1f)
        {
            Explode();
            return;
        }

        Vector3 endPos = target.transform.position;
        Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
        currentPos.y += arcHeight * 4 * t * (1 - t);
        transform.position = currentPos;
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
            }
        }

        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
