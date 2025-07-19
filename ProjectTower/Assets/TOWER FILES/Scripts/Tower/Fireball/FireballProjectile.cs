using System.Collections;
using UnityEngine;

public class FireballProjectile : Projectile
{
    private Unit target;

    [Header("Burn Settings")]
    [SerializeField] private float burnDamagePerSecond = 1f;
    [SerializeField] private int burnDuration = 3;

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
            StartCoroutine(ApplyBurn(target));
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
        }
    }


    private IEnumerator ApplyBurn(Unit enemy)
    {
        for (int i = 0; i < burnDuration; i++)
        {
            yield return new WaitForSeconds(1f);

            if (enemy != null)
            {
                enemy.TakeDamage(burnDamagePerSecond);
                enemy.Die();
            }
        }

        Destroy(gameObject);
    }
}
