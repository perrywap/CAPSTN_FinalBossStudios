using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public int index;
    public Image cardImage;

    public bool isPicked;

    private void Start()
    {
        index = Random.Range(0, PersistentData.Instance.unitDatas.Count);
        cardImage.sprite = PersistentData.Instance.unitDatas[index].unitSprite;
    }

    public virtual void OnCardClicked()
    {
        isPicked = true;

        RewardsPanel.Instance.RewardPicked();
        HudManager.Instance.FadeOut();
    }
}