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

        Debug.Log("Should open chest");
        this.GetComponent<Animator>().SetTrigger("Open");

        StartCoroutine(OnRewardReceived());
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

    private IEnumerator OnRewardReceived()
    {
        if (LevelNode.LastClickedNode != null)
        {
            var levelName = LevelNode.LastClickedNode.levelSceneName;

            GameProgress.Instance.MarkLevelCompleted(levelName);

            if (LevelNode.LastClickedNode.nextLevels != null)
            {
                foreach (var level in LevelNode.LastClickedNode.nextLevels)
                {
                    GameProgress.Instance.UnlockLevel(level);
                }
            }

            LevelNode[] allNodes = FindObjectsOfType<LevelNode>();
            foreach (var node in allNodes)
            {
                node.isUnlocked = GameProgress.Instance.IsUnlocked(node.levelSceneName);
                bool isCompleted = GameProgress.Instance.IsCompleted(node.levelSceneName);

                if (isCompleted && node.completedSprite != null)
                {
                    node.GetComponent<SpriteRenderer>().sprite = node.completedSprite;
                }

                foreach (Transform child in node.transform)
                {
                    if (child.GetComponent<ParticleSystem>())
                    {
                        Destroy(child.gameObject);
                    }
                }

                if (node.isUnlocked && !isCompleted && node.unlockedVFXPrefab != null)
                {
                    Instantiate(node.unlockedVFXPrefab, node.transform.position, Quaternion.identity, node.transform);
                }
            }
        }
        yield return new WaitForSeconds(4f);
        UILevelOverlayManager.Instance.Hide();
    }
}