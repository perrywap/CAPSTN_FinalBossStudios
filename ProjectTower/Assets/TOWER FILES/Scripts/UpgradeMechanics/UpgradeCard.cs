using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public UnitData unitToUpgrade;
    public int index;

    private void Start()
    {
        
    }

    public virtual void OnCardClicked()
    {
        index = Random.Range(0, PersistentData.Instance.unitDatas.Count);
    }
}
