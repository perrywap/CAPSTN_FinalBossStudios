using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public int index;
    public Image cardImage;

    public bool isClickable;
    public bool isPicked;
    public bool isChestCard;

    private void Start()
    {
        if (isChestCard)
            return;

        index = Random.Range(0, PersistentData.Instance.unitDatas.Count);
        cardImage.sprite = PersistentData.Instance.unitDatas[index].unitSprite;
    }

    public virtual void Activate(int dataIndex)
    {
        
    }

    public virtual void OnCardClicked()
    {
        if (!isClickable)
            return;

        if (isPicked)
            return;

        isPicked = true;

        RewardsPanel.Instance.RewardPicked();
        HudManager.Instance.FadeOut();
        Activate(index);
    }
}