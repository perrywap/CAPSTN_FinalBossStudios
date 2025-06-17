using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPanel : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    private void Update()
    {
        //DisplayCards();
    }

    private void DisplayCards()
    {
        if (PersistentData.Instance.unitsToDeploy == null)
            return;
        
        for (int i = 0; i < PersistentData.Instance.unitsToDeploy.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab);
            newCard.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            newCard.transform.SetParent(this.transform);
            newCard.GetComponent<Card>().UnitPrefab = PersistentData.Instance.unitsToDeploy[i];
        }
    }
}
