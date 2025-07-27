using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Unit
{
    public override void Die()
    {
        isDead = true;

        this.gameObject.GetComponent<Unit>().State = UnitState.ATTACKING;
        this.gameObject.GetComponent<Animator>().SetTrigger("explode");
    }
}
