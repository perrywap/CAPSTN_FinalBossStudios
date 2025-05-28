using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTower : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    private float _hp, _maxHp;

    [SerializeField]
    private Image _hpBar;

    #endregion

    #region UNITY METHODS
    private void Update()
    {
        
    }
    #endregion

    #region METHODS

    private void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp < 0)
        {
            _hp = 0;
        }

        if (_hpBar != null)
        {
            _hpBar.fillAmount = _hp / _maxHp;
        }
    }

    #endregion

    #region ONTRIGGER EVENTS
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            TakeDamage(enemy.Damage);
        }
    }

    #endregion
}
