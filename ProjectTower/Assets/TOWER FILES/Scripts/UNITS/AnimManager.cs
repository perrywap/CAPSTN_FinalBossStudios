using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Unit unit;
    [SerializeField] private UnitState state;
    [SerializeField] private bool isWalking, isSeeking, isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        state = unit.State;
    }

    private void Update()
    {
        isWalking = (state == UnitState.WALKING) ? true : false;
        isSeeking = (state == UnitState.SEEKING) ? true : false;
        isAttacking = (state == UnitState.ATTACKING) ? true : false;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isSeeking", isSeeking);
        animator.SetBool("isAttacking", isAttacking);
    }
}
