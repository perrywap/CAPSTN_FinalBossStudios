using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    private float _hp;

    [SerializeField]
    private float _damagePerHit;

    [SerializeField]
    private float _hitRate;

    [SerializeField]
    private int _buildCost;

    [SerializeField]
    private List<Unit> _enemiesInRange;

    #endregion

    #region METHODS

    #endregion

    #region ONTRIGGEREVENTS
    private void OnTriggerEnter(Collider other)
    {
        Unit enemy = other.GetComponent<Unit>();

        if(enemy != null)
        {
            _enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Unit enemy = other.GetComponent<Unit>();

        if (enemy != null)
        {
            _enemiesInRange.Remove(enemy);
        }
    }
    #endregion
}
