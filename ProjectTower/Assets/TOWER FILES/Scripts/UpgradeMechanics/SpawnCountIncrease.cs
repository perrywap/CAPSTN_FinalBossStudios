using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCountIncrease : UpgradeCard
{
    [SerializeField] private int amount;

    public override void OnCardClicked()
    {
        base.OnCardClicked();
        UnitData unit = PersistentData.Instance.unitDatas[index];

        unit.SpawnCount += amount;
    }
}
