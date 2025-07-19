using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Transform target;

    [Header("OnTRIGGER EVENTS")]
    [SerializeField] private CustomTrigger detectionRangeTrigger;
    [SerializeField] private CustomTrigger attackRangeTrigger;

    private Unit unit;
    private Tower currentTargetTower;
    private Coroutine attackCoroutine;
    private bool isKnockedback = false;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        detectionRangeTrigger.EnteredTrigger += OnDetectionRangeEnter;
        detectionRangeTrigger.ExitTrigger += OnDetectionRangeExit;

        attackRangeTrigger.EnteredTrigger += OnAttackRangeEnter;
        attackRangeTrigger.ExitTrigger += OnAttackRangeExit;
    }

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (unit.State == UnitState.SEEKING && !isKnockedback)
        {
            Seek();
        }
    }
    #endregion

    #region METHODS
    public void Seek()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, unit.Speed * Time.deltaTime);
    }

    private IEnumerator Attack(Tower target)
    {
        while (target != null)
        {
            yield return new WaitForSecondsRealtime(attackSpeed); // wait BEFORE attacking

            if (isKnockedback)
                yield break;

            target.TakeDamage(unit.Damage);
            Debug.Log($"{gameObject.name} attacked {target.name} for {unit.Damage} damage. Tower HP: {target.Hp}");

            if (target.Hp <= 0)
            {
                Debug.Log($"{target.name} has been destroyed!");
                currentTargetTower = null;
                this.target = null;
                unit.State = UnitState.WALKING;
                yield break;
            }
        }
    }

    public void StartKnockback()
    {
        isKnockedback = true;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        unit.State = UnitState.WALKING;
    }

    public void TryResumeCombatAfterKnockback()
    {
        isKnockedback = false;

        float detectionRadius = detectionRangeTrigger.GetComponent<CircleCollider2D>().radius;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        Tower closestTower = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            Tower tower = hit.GetComponent<Tower>();
            if (tower == null) continue;

            float dist = Vector2.Distance(transform.position, tower.transform.position);
            if (dist < closestDist)
            {
                closestTower = tower;
                closestDist = dist;
            }
        }

        if (closestTower != null)
        {
            target = closestTower.transform;
            currentTargetTower = closestTower;

            if (closestDist <= unit.AttackRange)
            {
                unit.State = UnitState.ATTACKING;
                attackCoroutine = StartCoroutine(Attack(currentTargetTower));
            }
            else
            {
                unit.State = UnitState.SEEKING;
            }
        }
        else
        {
            unit.State = UnitState.WALKING;
            target = null;
            currentTargetTower = null;
        }
    }
    #endregion

    #region TRIGGER EVENTS
    //public virtual void OnDetectionRangeEnter(Collider2D col)
    //{
    //    Tower tower = col.GetComponent<Tower>();

    //    if (tower != null)
    //    {

    //        unit.State = UnitState.SEEKING;
    //        target = tower.transform;
    //    }
    //}

    //public virtual void OnDetectionRangeExit(Collider2D col)
    //{
    //    unit.State = UnitState.WALKING;
    //}

    //public virtual void OnAttackRangeEnter(Collider2D col) 
    //{
    //    if(target != null)
    //    {
    //        unit.State = UnitState.ATTACKING;
    //        StartCoroutine(Attack(col.GetComponent<Tower>()));
    //    }  
    //}
    //public virtual void OnAttackRangeExit(Collider2D col)
    //{
    //    unit.State = UnitState.WALKING;
    //}

    public void OnDetectionRangeEnter(Tower col)
    {
        if (col != null && !isKnockedback)
        {
            target = col.transform;
            currentTargetTower = col;
            unit.State = UnitState.SEEKING;
        }
    }

    public void OnDetectionRangeExit(Tower col)
    {
        if (col == currentTargetTower)
        {
            target = null;
            currentTargetTower = null;
            unit.State = UnitState.WALKING;
        }
    }

    public void OnAttackRangeEnter(Tower col)
    {
        if (col != null && !isKnockedback)
        {
            target = col.transform;
            currentTargetTower = col;
            unit.State = UnitState.ATTACKING;

            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            attackCoroutine = StartCoroutine(Attack(col));
        }
    }

    public void OnAttackRangeExit(Tower col)
    {
        if (col == currentTargetTower)
        {
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            target = null;
            currentTargetTower = null;
            unit.State = UnitState.WALKING;
        }
    }
    #endregion
}
