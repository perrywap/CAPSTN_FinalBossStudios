using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUP : UpgradeCard
{
    [SerializeField] private float amount;

    public override void OnCardClicked()
    {
        base.OnCardClicked();
        UnitData unit = PersistentData.Instance.unitDatas[index];

        unit.Damage += amount;
    }
}
