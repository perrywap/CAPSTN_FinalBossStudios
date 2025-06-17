using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int manaCount;

    [SerializeField] private string[] nextLevelNames;

    public void WinLevel()
    {
        if (GameProgress.Instance != null)
        {
            foreach (string nextLevel in nextLevelNames)
            {
                GameProgress.Instance.UnlockLevel(nextLevel);
            }
        }

        SceneManager.LoadScene("LevelSelect");
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinLevel();
        }
    }
}
