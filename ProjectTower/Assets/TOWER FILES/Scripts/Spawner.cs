using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour, IDropHandler
{
    [SerializeField] private float spawnRate;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            
            GameObject unit = eventData.pointerDrag.GetComponent<Card>().SummonPrefab;
            int spawnCount = eventData.pointerDrag.GetComponent<Card>().SummonPrefab.GetComponent<Unit>().SpawnCount;
            StartCoroutine(StartSpawner(unit, spawnCount));
            Destroy(eventData.pointerDrag);
        }
    }

    public IEnumerator StartSpawner(GameObject unitToSpawn, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            
            Instantiate(unitToSpawn);
            yield return new WaitForSecondsRealtime(spawnRate);
        }

    }
}