using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private GameObject wpManager;

    [SerializeField] private Transform[] points;

    [SerializeField] private int pointIndex;

    private Unit unit;

    private void Start()
    {
        unit = this.GetComponent<Unit>();

        wpManager = GameObject.FindGameObjectWithTag("WaypointManager");

        points = wpManager.GetComponent<Waypoint>().waypoints;

        transform.position = points[pointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (unit.State != UnitState.WALKING)
            return;

        if (pointIndex <= points.Length - 1)
        {

            transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, unit.Speed * Time.deltaTime);

            if (transform.position == points[pointIndex].transform.position)
            {
                pointIndex++;
            }

            if (pointIndex == points.Length)
                unit.OnPathComplete();
        }
    }
}