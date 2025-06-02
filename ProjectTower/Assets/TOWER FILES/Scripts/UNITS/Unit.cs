using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UnitType
{
    Normal,
    Tank,
    Runner,
    Flying
}

public class Unit : MonoBehaviour
{
    #region VARIABLES

    [Header("ENEMY STATS")]
    [SerializeField] private string _name;

    [SerializeField] private float _hp;

    [SerializeField] private float _armor;

    [SerializeField] private float _speed;

    [SerializeField] private float _damage;

    [SerializeField] private int _rewardOnKill;

    [SerializeField] private int _manaCost;

    [SerializeField] private int _spawnCount;

    [SerializeField] private UnitType _type;

    //[SerializeField] private Animator _anim;

    [Header("PATHFIND SETTINGS")]
    [SerializeField] private GameObject wpManager;

    [SerializeField] private Transform[] points;

    [SerializeField] private int pointIndex;


    #endregion

    #region GETTERS AND SETTERS
    public string Name { get { return _name; } }
    public float Hp { get { return _hp; } }
    public int ManaCost { get { return _manaCost; } }
    public int SpawnCount { get { return _spawnCount; } }
    public UnitType Type { get { return _type; } }
    public GameObject WaypointManger { get { return wpManager; } set { wpManager = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }

    #endregion

    #region UNITY METHODS
    private void Start()
    {
        //_anim = GetComponent<Animator>();

        points = wpManager.GetComponent<Waypoint>().waypoints;

        transform.position = points[pointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region METHODS
    private void Move()
    {
        
        if (pointIndex <= points.Length - 1)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, _speed * Time.deltaTime);

            if (transform.position == points[pointIndex].transform.position)
            {
                pointIndex++;
            }

            if (pointIndex == points.Length)
                OnPathComplete();
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
