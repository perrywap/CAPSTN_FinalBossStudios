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
            if (eventData.pointerDrag.GetComponent<Card>().SummonPrefab.
                GetComponent<Unit>().ManaCost > GameManager.Instance.CurrentMana)
            {
                return;
            }
            else
            {
                GameManager.Instance.cardsOnHand.Remove(eventData.pointerDrag.gameObject);
                GameObject unit = eventData.pointerDrag.GetComponent<Card>().SummonPrefab;
                GameManager.Instance.UseMana(unit.GetComponent<Unit>().ManaCost);

                int spawnCount = eventData.pointerDrag.GetComponent<Card>().SummonPrefab.GetComponent<Unit>().SpawnCount;
                StartCoroutine(StartSpawner(unit, spawnCount));
                Destroy(eventData.pointerDrag);
            }
        }
    }

    public IEnumerator StartSpawner(GameObject unitToSpawn, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject unit = Instantiate(unitToSpawn);
            GameManager.Instance.unitsOnField.Add(unit);

            yield return new WaitForSecondsRealtime(spawnRate);
        }

    }
}