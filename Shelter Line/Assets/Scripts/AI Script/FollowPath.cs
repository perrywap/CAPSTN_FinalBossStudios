using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    [SerializeField] private GameObject wpManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float accuracy = 0.5f;
    [SerializeField] private float rotationSpeed = 5f;

    private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        waypoints = wpManager.GetComponent<WaypointManager>().waypoints;
        if (waypoints.Length == 0) return;

        transform.position = waypoints[0].transform.position;
        currentWaypointIndex = 1; 
    }

    void Update()
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
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnPathComplete()
    {
        // Add logic here (reduce player life, destroy self)
        Debug.Log($"{gameObject.name} reached the end!");
        Destroy(gameObject);
    }
}
