using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("REFERENCES")]
    [SerializeField] private GameObject summonPanel;
    [SerializeField] private bool isGameOver;


    [SerializeField] private int cardOnHand;

    private void Start()
    {
        isGameOver = false;
        
        
    }

    private void Update()
    {
        
    }
}
