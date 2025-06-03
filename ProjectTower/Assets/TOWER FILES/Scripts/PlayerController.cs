using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Opening selection panel");
            OpenSelectionPanel();
        }
    }

    public void OpenSelectionPanel()
    {
        selectionPanel.SetActive(!selectionPanel.activeSelf);
    }

    public void OnSpawnBtnClicked()
    {
        Spawner.Instance.Spawn();
        
        OpenSelectionPanel();
    }
}
