using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float lastAttackTime;
    [SerializeField] private Transform target;


    [Header("OnTRIGGER EVENTS")]
    [SerializeField] private CustomTrigger detectionRangeTrigger;
    [SerializeField] private CustomTrigger attackRangeTrigger;

    private Unit unit;
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
        unit = this.gameObject.GetComponent<Unit>();
    }

    private void Update()
    {
        if (unit.State == UnitState.SEEKING)
        {
            Seek();
        }
    }
    #endregion

    #region METHODS
    public virtual void Seek()
    {
        if (target == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, unit.Speed * Time.deltaTime);
    }

    private IEnumerator Attack(Tower target)
    {
        while (target != null)
        {
            target.TakeDamage(unit.Damage);
            Debug.Log($"{gameObject.name} attacked {target.name} for {unit.Damage} damage. Tower HP: {target.Hp}");

            if (target.Hp <= 0)
            {
                Debug.Log($"{target.name} has been destroyed!");
                target = null;
            }

            yield return new WaitForSecondsRealtime(attackSpeed);
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

    public virtual void OnDetectionRangeEnter(Tower col)
    {
        if (col != null)
        {
            unit.State = UnitState.SEEKING;
            target = col.transform;
        }
    }

    public virtual void OnDetectionRangeExit(Tower col)
    {
        //if(col == null)
        //    unit.State = UnitState.WALKING;
        target = null;
        unit.State = UnitState.WALKING;
    }

    public virtual void OnAttackRangeEnter(Tower col)
    {
        if (target != null)
        {
            unit.State = UnitState.ATTACKING;
            StartCoroutine(Attack(col.GetComponent<Tower>()));
        }
    }
    public virtual void OnAttackRangeExit(Tower col)
    {
        target = null;
        unit.State = UnitState.WALKING;
    }
    #endregion
}
