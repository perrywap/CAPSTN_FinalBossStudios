using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int manaCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }
}
