using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Text unitName;

    [SerializeField] private Text unitManaCost;

    [SerializeField] private Text unitHp;

    [SerializeField] private Image unitSprite;

    [SerializeField] private Image unitType;

    [SerializeField] private Sprite[] TypeIcons;

    [SerializeField] private GameObject unitPrefab;

    [SerializeField] private Transform deployPanelPos;

    [SerializeField] private Transform cardPanelPos;

    [SerializeField] private bool isClicked;

    public Image unitToDeploy;

    public GameObject UnitPrefab {  get { return unitPrefab; } set { unitPrefab = value; } }
    public bool IsClicked { get { return isClicked; } set { isClicked = value; } }

    private void Start()
    {
        isClicked = false;

        unitName.text = unitPrefab.GetComponent<Unit>().Name.ToString();
        unitSprite.sprite = unitPrefab.GetComponent<SpriteRenderer>().sprite;
        unitHp.text = unitPrefab.GetComponent<Unit>().Hp.ToString();
        unitManaCost.text = unitPrefab.GetComponent<Unit>().ManaCost.ToString();

        if (unitPrefab.GetComponent<Unit>().Type == UnitType.Normal)
            unitType.sprite = TypeIcons[0];

        if (unitPrefab.GetComponent<Unit>().Type == UnitType.Tank)
            unitType.sprite = TypeIcons[1];
        
        if (unitPrefab.GetComponent<Unit>().Type == UnitType.Runner)
            unitType.sprite = TypeIcons[2];
        
        if (unitPrefab.GetComponent<Unit>().Type == UnitType.Flying)
            unitType.sprite = TypeIcons[3];

        cardPanelPos = GameObject.FindGameObjectWithTag("CardPanel").transform;
        deployPanelPos = GameObject.FindGameObjectWithTag("DeployPanel").transform;
    }

    public void OnCardClicked()
    {
        if (!isClicked)
        {
            if (GameManager.Instance.manaCount <= 0)
                return;
                       
            AddToDeployPanel();
            GameManager.Instance.manaCount -= unitPrefab.GetComponent<Unit>().ManaCost;
        }

        else if (isClicked)
        {
            AddToCardPanel();
            GameManager.Instance.manaCount += unitPrefab.GetComponent<Unit>().ManaCost;
        }
    }

    public void AddToDeployPanel()
    {
        isClicked = true;
        this.transform.SetParent(deployPanelPos.transform);
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        PersistentData.Instance.unitsToDeploy.Add(unitPrefab);
    }

    public void AddToCardPanel()
    {
        isClicked = false;
        this.transform.SetParent(cardPanelPos.transform);
        this.transform.localScale = Vector3.one;
        PersistentData.Instance.unitsToDeploy.Remove(unitPrefab);
    }
}
