using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public UnitData unitToUpgrade;
    public int index;
    public List<Sprite> unitImages = new List<Sprite>();
    public Image unitImage;

    public bool isPicked;

    public bool stopRotation;

    private void Start()
    {
        for (int i = 0; i < PersistentData.Instance.unitDatas.Count; i++)
        {
            unitImages.Add(PersistentData.Instance.unitDatas[i].unitSprite);
        }
    }

    public virtual void OnCardClicked()
    {
        if(stopRotation)
            return; 

        isPicked = true;

        RewardsPanel.Instance.RewardPicked();

        StartCoroutine(RandomPickUnit());
        StartCoroutine(UnitRotation());
    }

    public IEnumerator RandomPickUnit()
    {
        index = Random.Range(0, unitImages.Count);
        while (!stopRotation)
        {
            yield return new WaitForSecondsRealtime(.10f);
            Debug.Log("Rolling");
            unitImage.sprite = unitImages[index];

            index++;
            if (index == unitImages.Count)
            {
                index = 0;
            }
        }
    }

    public IEnumerator UnitRotation()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("Stopped");
        stopRotation = true;
    }
}