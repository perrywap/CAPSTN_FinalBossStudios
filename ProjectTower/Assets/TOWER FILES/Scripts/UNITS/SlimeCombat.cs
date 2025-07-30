using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeCombat : UnitCombat
{
    [SerializeField] private List<Transform> targets = new List<Transform>();
    private bool hasExploded = false;
    [SerializeField] private GameObject explosionVFXPrefab;

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }

        foreach (Transform t in targets.ToList())
        {
            t.GetComponent<Tower>().TakeDamage(this.gameObject.GetComponent<Unit>().Damage);                
        }

        GameManager.Instance.unitsOnField.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void PlayExplosionAnimation()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("explode");
        }
    }

    public override void HandleDeath()
    {
        if (hasExploded) return;

        PlayExplosionAnimation();
        enabled = false;
    }

    public override void OnAttackRangeEnter(Tower col)
    {
        targets.Add(col.transform);    
    }

    public override void OnAttackRangeExit(Tower col)
    {
        targets.Remove(col.transform);
    }
}
