using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    public static PreviewSystem Instance { get; private set; }

    public GameObject previewPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (previewPrefab == null)
            return;
    }

    public void ShowPreview(GameObject previewObject, Vector2Int size)
    {
        Destroy(previewPrefab);
        previewPrefab = Instantiate(previewObject);
    }
}
