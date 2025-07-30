using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private CircleCollider2D detectionRangeCollider, attackRangeCollider;

    [Header("OnTRIGGER EVENTS")]
    [SerializeField] private CustomTrigger detectionRangeTrigger;
    [SerializeField] private CustomTrigger attackRangeTrigger;

    private Unit unit;
    private Coroutine attackCoroutine;
    private AudioSource walkingAudioSource;
    private bool isWalkingSoundPlaying = false;
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

        walkingAudioSource = gameObject.AddComponent<AudioSource>();
        walkingAudioSource.loop = true;
        walkingAudioSource.playOnAwake = false;
        walkingAudioSource.outputAudioMixerGroup = FindObjectOfType<AudioController>()?.soundSource.outputAudioMixerGroup;
    }

    private void Update()
    {
        attackRangeCollider.radius = unit.AttackRange;

        if (unit.State == UnitState.SEEKING)
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
        AudioController audioController = FindObjectOfType<AudioController>();
        AudioClip attackSound = unit.AttackSound;

        while (target != null)
        {
            unit.GetComponent<Animator>().SetTrigger("attack");
            target.TakeDamage(unit.Damage);

            if (attackSound != null && audioController != null)
            {
                audioController.PlayAudio(null, attackSound);
            }

            yield return new WaitForSecondsRealtime(attackSpeed);
        }
    }
    #endregion

    #region TRIGGER EVENTS
    public void OnDetectionRangeEnter(Tower col)
    {
        if (col != null)
        {
            target = col.transform;
            unit.State = UnitState.SEEKING;
        }
    }

    public void OnDetectionRangeExit(Tower col)
    {
        target = null;
        unit.State = UnitState.WALKING;
    }

    public virtual void OnAttackRangeEnter(Tower col)
    {
        if (col != null)
        {
            unit.State = UnitState.ATTACKING;

            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            attackCoroutine = StartCoroutine(Attack(col));
        }
    }

    public virtual void OnAttackRangeExit(Tower col)
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        target = null;
        unit.State = UnitState.WALKING;
    }
    #endregion
}
