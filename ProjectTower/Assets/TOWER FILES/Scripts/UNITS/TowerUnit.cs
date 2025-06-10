using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private float damagePerHit;
    
    [SerializeField] private float fireRate;
    
    [SerializeField] private float attackRange;
    
    [SerializeField] private bool canAttack;

    [SerializeField] private bool isAttacking;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform currentTarget;

    [SerializeField] private List<GameObject> targets = new List<GameObject>();

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count != 0)
        {
            
        }
    }

    public virtual void Attack()
    {
        if (canAttack) { }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject unit = collision.GetComponent<GameObject>();

        if (unit != null)
        {
            targets.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject unit = collision.GetComponent<GameObject>();

        if (unit != null)
        {
            targets.Remove(unit);
        }
    }
}
