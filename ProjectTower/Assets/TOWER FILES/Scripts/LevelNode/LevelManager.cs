using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinLevel();
        }
    }
}
