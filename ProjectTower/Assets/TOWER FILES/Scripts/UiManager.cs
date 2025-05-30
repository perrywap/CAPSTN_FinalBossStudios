using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject[] cardSlots;

    private void Start()
    {
        DisplayCards();
    }

    private void DisplayCards()
    {
        for(int i = 0; i < cardManager.cards.Count; i++)
        {

        }
    }
}
