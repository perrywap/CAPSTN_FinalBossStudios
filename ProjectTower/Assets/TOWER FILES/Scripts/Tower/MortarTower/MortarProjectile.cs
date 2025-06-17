using UnityEngine;

public class MortarProjectile : Projectile
{
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float arcHeight = 2f;
    private float travelTime = 1f;
    private float elapsed = 0f;

    public void SetTarget(Vector3 position, float dmg)
    {
        base.SetTarget(null, dmg);
        startPosition = transform.position;
        targetPosition = position;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / travelTime;

        if (t >= 1f)
        {
            Explode();
            return;
        }

        Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);
        float height = arcHeight * Mathf.Sin(Mathf.PI * t);
        currentPos.y += height;

        transform.position = currentPos;
    }

    private void Explode()
    {
        float explosionRadius = 1.5f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            Unit enemy = hit.GetComponent<Unit>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.Die();
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
