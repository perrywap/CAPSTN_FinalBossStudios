using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private GameObject waypointManager;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
        StartCoroutine(StartSpawner());
    }

    public IEnumerator StartSpawner()
    {
        for(int i = 0; i < PersistentData.Instance.unitsToDeploy.Count; i++)
        {
            for(int j = 0; j < PersistentData.Instance.unitsToDeploy[i].GetComponent<Enemy>().SpawnCount; j++)
            {
                PersistentData.Instance.unitsToDeploy[i].GetComponent<Enemy>().WaypointManger = waypointManager;
                Instantiate(PersistentData.Instance.unitsToDeploy[i]);
                yield return new WaitForSecondsRealtime(.5f);
            }
        }
    }
}
