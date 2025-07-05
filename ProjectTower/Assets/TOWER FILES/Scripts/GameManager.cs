using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("MANA MANAGEMENT")]
    [SerializeField] private Image manaImage;
    [SerializeField] private Text manatext;
    [SerializeField] private float currentMana;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegenAmount;
    [SerializeField] private float manaRegenRate = 5f;

    public float CurrentMana { get { return currentMana; } }

    private float lastRegen;

    [Header("WIN/LOSE")]
    public GameObject winPanel;
    public GameObject losePanel;
    [SerializeField] private int cardsOnHandCount;
    [SerializeField] private int unitsOnFieldCount;
    public List<GameObject> unitsOnField = new List<GameObject>();
    public List<GameObject> cardsOnHand = new List<GameObject>();
    public bool isGameFinished;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentMana = maxMana;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        isGameFinished = false;
    }

    private void Update()
    {
        ManaManager();

        cardsOnHandCount = cardsOnHand.Count;
        unitsOnFieldCount = unitsOnField.Count;

        if (cardsOnHand.Count == 0 && unitsOnField.Count == 0)
        {
            losePanel.SetActive(true);
            isGameFinished = true;
        }
    }

    #region MANA MANAGEMENT
    private void ManaManager()
    {
        if(currentMana > maxMana)
            currentMana = maxMana;

        if(currentMana < maxMana)
            RegenMana();

        if (currentMana < 0)
            currentMana = 0;

        manatext.text = currentMana.ToString();
        manaImage.fillAmount = currentMana / maxMana;
    }

    private void RegenMana()
    {
        if(Time.time - lastRegen > manaRegenRate)
        {
            currentMana += manaRegenAmount;
            lastRegen = Time.time;
        }
    }

    public void UseMana(int cost)
    {
        Debug.Log(currentMana);
        currentMana -= cost;
        Debug.Log(currentMana);
    }
    #endregion

    #region UNIT MANAGEMENT
    
    #endregion

}
