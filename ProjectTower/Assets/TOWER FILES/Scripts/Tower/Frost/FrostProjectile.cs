using System.Collections;
using UnityEngine;

public class FrostProjectile : Projectile
{
    [Header("AOE Settings")]
    [SerializeField] private float explosionRadius = 1.5f;

    [Header("Slow Settings")]
    [SerializeField] private float slowAmount = 1f;
    [SerializeField] private float slowDuration = 3f;

    [Header("Animation Settings")]
    [SerializeField] private Sprite[] animationFrames;
    [SerializeField] private float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Unit target;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animationFrames.Length > 0)
        {
            StartCoroutine(PlayAnimation());
        }
    }

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

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
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

        spriteRenderer.enabled = false;
        enabled = false;
        Destroy(gameObject, slowDuration + 0.1f);
    }

    private IEnumerator ApplyTemporarySlow(Unit unit)
    {
        if (unit == null) yield break;

        float actualSlow = Mathf.Min(slowAmount, unit.Speed);
        unit.Speed -= actualSlow;

        yield return new WaitForSeconds(slowDuration);

        if (unit != null)
        {
            unit.Speed += actualSlow;
        }
    }

    private IEnumerator PlayAnimation()
    {
        int index = 0;

        while (true)
        {
            if (spriteRenderer != null && animationFrames.Length > 0)
            {
                spriteRenderer.sprite = animationFrames[index];
                index = (index + 1) % animationFrames.Length;
            }

            yield return new WaitForSeconds(animationSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
