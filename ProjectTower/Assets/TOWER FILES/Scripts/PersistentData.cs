using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance { get; private set; }

    [Header("UNITS")]
    public List<GameObject> unitsOwned = new List<GameObject>();
    public List<UnitData> unitDatas = new List<UnitData>();

    [Header("Scene Load")]
    [SerializeField] private string scene;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
    }
}
