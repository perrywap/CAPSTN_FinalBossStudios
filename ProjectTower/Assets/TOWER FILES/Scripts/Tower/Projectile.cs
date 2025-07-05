using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Unit target;
    private float damage;

    public void SetTarget(Unit newTarget, float dmg)
    {
        target = newTarget;
        damage = dmg;
    }

    private void Update()
    {
        if (target == null)
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
