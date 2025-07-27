using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slime : UnitCombat
{
    [SerializeField] private List<Transform> targets = new List<Transform>();

    public bool isDead;

    private void FixedUpdate()
    {
        if (this.gameObject.GetComponent<Unit>().Hp <= 40)
        {
            this.gameObject.GetComponent<Unit>().State = UnitState.ATTACKING;
            this.gameObject.GetComponent<Animator>().SetTrigger("explode");
        }
    }

    public void Explode()
    {
        isDead = true;
        foreach (Transform t in targets.ToList())
        {
            t.GetComponent<Tower>().TakeDamage(this.gameObject.GetComponent<Unit>().Damage);                
        }
        if (isDead)
        {
            
        }
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
