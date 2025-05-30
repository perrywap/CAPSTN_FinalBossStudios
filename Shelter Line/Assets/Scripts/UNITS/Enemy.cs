using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VARIABLES

    [Header("ENEMY STATS")]
    [SerializeField]
    private float _hp;

    [SerializeField]
    private float _armor;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private int _rewardOnKill;

    private Animator _anim;

    [Header("PATHFIND SETTINGS")]
    [SerializeField]
    private GameObject wpManager;
    
    [SerializeField]
    private float accuracy = 0.5f;

    [SerializeField]
    private GameObject[] waypoints;
    
    [SerializeField]
    private int currentWaypointIndex = 0;

    #endregion

    #region GETTERS AND SETTERS
    public float Damage { get { return _damage; } set { _damage = value; } }

    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _anim = GetComponent<Animator>();

        waypoints = wpManager.GetComponent<WaypointManager>().waypoints;
        if (waypoints.Length == 0) return;

        transform.position = waypoints[0].transform.position;
        currentWaypointIndex = 1;
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region METHODS
    private void Move()
    {
        if (currentWaypointIndex >= waypoints.Length)
            return;

        Transform target = waypoints[currentWaypointIndex].transform;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude < accuracy)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                OnPathComplete();
                return;
            }
        }
        else
        {
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * _speed * Time.deltaTime;
        }
    }

    private void OnPathComplete()
    {
        // Add logic here (reduce player life, destroy self)
        Debug.Log($"{gameObject.name} reached the end!");
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        _hp -= damage;
        
        if( _hp < 0 )
        {
            _hp = 0;
        }
    }

    public virtual void Die()
    {
        if (_hp > 0)
            return;
        
        //_anim.SetBool("isDead", true);
        Destroy(gameObject);
    }
    #endregion
}
