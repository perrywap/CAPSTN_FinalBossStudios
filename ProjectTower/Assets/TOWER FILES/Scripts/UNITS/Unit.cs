using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float _attackRange = 1.5f; 
    [SerializeField] private int _rewardOnKill;
    [SerializeField] private int _manaCost;
    [SerializeField] private int _spawnCount;
    [SerializeField] private UnitType _type;

    public int AttackDamage = 10;

    #endregion

    #region GETTERS AND SETTERS

    public string Name => _name;
    public float Hp => _hp;
    public float Speed => _speed;
    public float AttackRange => _attackRange; 
    public int ManaCost => _manaCost;
    public int SpawnCount => _spawnCount;
    public UnitType Type => _type;
    public float Damage { get => _damage; set => _damage = value; }

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        // Add initialization logic if needed
    }

    #endregion

    #region METHODS

    public void OnPathComplete()
    {
        Debug.Log($"{gameObject.name} reached the end!");
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp < 0)
        {
            _hp = 0;
        }
    }

    public virtual void Die()
    {
        if (_hp > 0) return;

        Destroy(gameObject);
    }

    #endregion
}
