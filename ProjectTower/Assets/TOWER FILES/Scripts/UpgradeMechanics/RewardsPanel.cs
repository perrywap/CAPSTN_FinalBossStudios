using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPanel : MonoBehaviour
{
    public static RewardsPanel Instance { get; private set; }

    [SerializeField] private List<UpgradeCard> upgradeCards = new List<UpgradeCard>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
