using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsPanel : MonoBehaviour
{
    public static RewardsPanel Instance { get; private set; }

    [Header("GOLD REWARDS")]
    [SerializeField] private int goldRewardAmount;
    [SerializeField] private Text goldTxt;

    [Header("REWARD CARDS")]
    [SerializeField] private GameObject rewardsGO;
    [SerializeField] private List<UpgradeCard> upgradeCards = new List<UpgradeCard>();
    [SerializeField] private int numberOfRewards = 3;


    private List<GameObject> cards = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        goldTxt.text = $"+{goldRewardAmount.ToString()}";

        rewardsGO.SetActive(false);
        PersistentData.Instance.gold += goldRewardAmount;        
    }

    public void ShowRewards()
    {
        rewardsGO.SetActive(true);

        cards = PersistentData.Instance.upgradeCards;

        for (int i = 0; i < numberOfRewards; i++)
        {
            GameObject cardGO = cards[Random.Range(0, cards.Count)];
            GameObject card = Instantiate(cardGO);
            card.transform.SetParent(rewardsGO.transform);
        }

        upgradeCards.AddRange(GetComponentsInChildren<UpgradeCard>());
    }

    public void RewardPicked()
    {
        for (int i = 0; i < upgradeCards.Count; i++)
        {
            upgradeCards[i].gameObject.SetActive(upgradeCards[i].isPicked ? true : false);
        }
    }
}
