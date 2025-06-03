using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject deployPanel;

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
            for(int j = 0; j < PersistentData.Instance.unitsToDeploy[i].GetComponent<Unit>().SpawnCount; j++)
            {
                Instantiate(PersistentData.Instance.unitsToDeploy[i], spawnPosition);
                yield return new WaitForSecondsRealtime(spawnRate);
            }            
        }
        foreach (Card card in deployPanel.GetComponentsInChildren<Card>())
        {
            card.AddToCardPanel();
        }
    }
}
