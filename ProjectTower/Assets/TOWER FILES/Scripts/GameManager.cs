using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("MANA MANAGEMENT")]
    [SerializeField] private Image manaImage;           // UI
    [SerializeField] private Text manatext;             // UI
    [SerializeField] private float currentMana;
    [SerializeField] private float maxMana;
    [SerializeField] private float regenAmount;
    [SerializeField] private float regenRate = 5f;

    public float CurrentMana { get { return currentMana; } }

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

        StartCoroutine(RegenerateMana());
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
        if(currentMana >= maxMana)
            currentMana = maxMana;            

        if (currentMana < 0)
            currentMana = 0;

        manatext.text = currentMana.ToString();
        manaImage.fillAmount = currentMana / maxMana;
    }

    public void UseMana(int cost)
    {
        currentMana -= cost;
        Debug.Log(currentMana);
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenRate);

            if (currentMana < maxMana)
            {
                currentMana += regenAmount;
            }
        }
    }
    #endregion


}
