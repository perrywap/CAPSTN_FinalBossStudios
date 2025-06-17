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

public enum UnitState
{
    Idle,
    Walking,
    Attacking
}

public class Unit : MonoBehaviour
{
    #region VARIABLES

    [Header("SUMMON STATS")]
    [SerializeField] private float _hp;
    [SerializeField] private float _armor;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private UnitType _type;

    [Header("SPAWN SETTINGS")]
    [SerializeField] private string unitName;
    [SerializeField] private int manaCost;
    [SerializeField] private int spawnCount;

    private UnitState state;
    #endregion

    #region GETTERS AND SETTERS
    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public UnitType Type { get { return _type; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public string UnitName { get { return unitName; } }
    public int ManaCost { get { return manaCost; } }
    public int SpawnCount { get { return spawnCount; } }
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        
    }

    #endregion

    #region METHODS

    public void OnPathComplete()
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
        
        Destroy(gameObject);
    }
    #endregion
}
