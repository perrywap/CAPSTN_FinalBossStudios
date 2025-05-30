using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Normal,
    Tank,
    Runner,
    Flying
}

[System.Serializable]
public class Card
{
    public string unitName;

    public Sprite unitSprite;
    public Sprite unitBgSprite;

    public UnitType unitType;

    public int unitManaCost;
    public int unitHp;

    public GameObject unitPrefab;
}
