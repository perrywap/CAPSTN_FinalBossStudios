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
        GameObject toadd = PersistentData.Instance.units[index];
        PersistentData.Instance.unitsOwned.Add(toadd);

        HudManager.Instance.FadeOut();
    }
}