using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    public string levelSceneName;
    public bool isUnlocked = false;

    private void Start()
    {
        isUnlocked = GameProgress.Instance != null && GameProgress.Instance.IsUnlocked(levelSceneName);

        GetComponent<SpriteRenderer>().color = isUnlocked ? Color.white : Color.gray;
    }

    private void OnMouseDown()
    {
        if (isUnlocked)
        {
            SceneManager.LoadScene(levelSceneName);
        }
    }
}
