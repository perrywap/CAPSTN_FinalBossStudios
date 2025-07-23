using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : UpgradeCard
{
    [SerializeField] private float amount;

    public override void OnCardClicked()
    {
        if (isPicked)
            return;

        base.OnCardClicked();
        UnitData unit = PersistentData.Instance.unitDatas[index];

        unit.Speed += amount;
    }
}
