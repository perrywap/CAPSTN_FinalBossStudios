using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [Header("MANA")]
    public Image manaBar;
    public Text manaText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        manaBar.fillAmount = GameManager.Instance.manaCount / 10;
        manaText.text = GameManager.Instance.manaCount.ToString();
    }
}
