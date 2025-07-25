using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewUnit : UpgradeCard
{
    private void Start()
    {
        if (PersistentData.Instance.units == null)
        {
            Debug.LogError("No Units!");
            return;
        }

        index = Random.Range(0, PersistentData.Instance.units.Count);
        cardImage.sprite = PersistentData.Instance.units[index].GetComponent<SpriteRenderer>().sprite;
    }

    public override void OnCardClicked()
    {
        if (isPicked)
            return;

        isPicked = true;
        RewardsPanel.Instance.RewardPicked();
        GameObject unitToAdd = PersistentData.Instance.units[index];
        UnitData dataToAdd = unitToAdd.GetComponent<Unit>().Data;
        PersistentData.Instance.unitsOwned.Add(unitToAdd);

        for (int i = 0; i < PersistentData.Instance.unitDatas.Count; i++)
        {
            if(dataToAdd.Name == PersistentData.Instance.unitDatas[i].Name)
            {
                break;
            }
            else if(dataToAdd.Name != PersistentData.Instance.unitDatas[i].Name && i == PersistentData.Instance.unitDatas.Count - 1)
            {
                PersistentData.Instance.unitDatas.Add(dataToAdd);
            }
        }

        HudManager.Instance.FadeOut();
    }
}