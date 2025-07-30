using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject rewardCard;
    [SerializeField] private Transform chestParent;

    private void Start()
    {

    }

    public void OpenChest()
    {

        Debug.Log("SHould open chest");
        this.GetComponent<Animator>().SetTrigger("Open");
    }

    public void SpawnCard()
    {
        rewardCard = PersistentData.Instance.upgradeCards[Random.Range(0, PersistentData.Instance.upgradeCards.Count)];
        UpgradeCard upgrade = rewardCard.GetComponent<UpgradeCard>();
        upgrade.cardType = CardType.CHEST;
        upgrade.isClickable = false;

        GameObject rewardGO = Instantiate(rewardCard, chestParent);
        rewardGO.transform.SetParent(chestParent);

        for (int i = 0; i < PersistentData.Instance.unitDatas.Count; i++)
        {
            upgrade.Activate(i);
        }
    }

    public IEnumerator OnRewardReceived()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(this.gameObject);
    }
}