using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] private float hp;
    [SerializeField] protected float range = 3f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float fireRate = 1f;

    [Header("Broken Tower")]
    [SerializeField] private GameObject brokenTowerPrefab;
    [SerializeField] private Transform brokenSpawnPoint;

    [Header("VFX")]
    [SerializeField] private GameObject deathVFXPrefab;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSFX;

    protected float fireCooldown = 0f;
    public List<Unit> targetsInRange = new List<Unit>();

    public Unit currentTarget;

    public float Hp { get { return hp; } }
    public bool IsDying { get; private set; } = false;

    protected virtual void Start()
    {
        UpgradeStatsIfHighTier();
    }

    protected virtual void Update()
    {
        fireCooldown -= Time.deltaTime;
        RemoveNullTargets();

        if (!IsValidTarget(currentTarget))
        {
            currentTarget = GetNearestTarget();
        }

        if (currentTarget != null && fireCooldown <= 0f)
        {
            Attack(currentTarget);
            fireCooldown = 1f / fireRate;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null && !targetsInRange.Contains(unit))
        {
            targetsInRange.Add(unit);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            float dist = Vector2.Distance(transform.position, unit.transform.position);
            if (dist > range + 0.1f)
            {
                targetsInRange.Remove(unit);

                if (unit == currentTarget)
                {
                    currentTarget = null;
                }
            }
        }
    }

    protected void RemoveNullTargets()
    {
        targetsInRange.RemoveAll(u => u == null);
    }

    protected bool IsValidTarget(Unit target)
    {
        if (target == null) return false;
        if (Vector2.Distance(transform.position, target.transform.position) > range + 0.1f) return false;
        if (target.State == UnitState.DEAD || target.isDead) return false;
        return true;
    }

    protected Unit GetNearestTarget()
    {
        Unit closest = null;
        float shortestDist = Mathf.Infinity;

        foreach (Unit u in targetsInRange)
        {
            float dist = Vector2.Distance(transform.position, u.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closest = u;
            }
        }

        return closest;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0f)
        {
            hp = 0f;
            Die();
        }

        this.gameObject.GetComponentInChildren<HpBar>().PopHpBar();
    }

    private void Die()
    {
        IsDying = true;
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (deathSFX != null)
        {
            AudioController audioController = FindObjectOfType<AudioController>();
            if (audioController != null)
            {
                audioController.PlayAudio(null, deathSFX);
            }
        }

        yield return new WaitForSeconds(0.2f);

        Vector3 spawnPosition = brokenSpawnPoint != null ? brokenSpawnPoint.position : transform.position;

        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, spawnPosition, Quaternion.identity);
        }

        if (brokenTowerPrefab != null)
        {
            Instantiate(brokenTowerPrefab, spawnPosition, Quaternion.identity);
        }

        Destroy(transform.root.gameObject);
    }

    public virtual void Attack(Unit target)
    {
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpgradeStatsIfHighTier()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        int tier = GameProgress.Instance != null
            ? GameProgress.Instance.GetLevelTier(currentSceneName)
            : -1;

        if (tier >= 6)
        {
            hp += 20f;
            damage += 5f;

            Debug.Log($"[Tower] Upgraded stats for level {tier}");
        }
    }
}
