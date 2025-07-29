using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalCoreNumberBased : PortalCore
{
    // A NUMBER OF UNITS MUST PASS THRU
    // IN ORDER TO WIN THE CURRENT LEVEL

    [SerializeField] private int unitsEntered = 0;
    [SerializeField] private int unitsRequired;


    private void Update()
    {
        if (unitsEntered >= unitsRequired)
        {
            GameEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            unitsEntered++;
        }
    }
}
