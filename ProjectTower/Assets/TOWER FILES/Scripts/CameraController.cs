using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Camera cam;

    [SerializeField] private float zoomStep,minCamSize, maxCamSize;

    [SerializeField] private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private Vector3 dragOrigin;

    public bool canMoveCam = true;

    private void Awake()
    {
        Instance = this;

        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();

        if(Input.mouseScrollDelta.y > 0)
            ZoomIn();
        if (Input.mouseScrollDelta.y < 0)
            ZoomOut();
    }

    private void PanCamera()
    {
        if(!canMoveCam)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Should pan");
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
            

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //move the camera by that distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;

        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x , minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canMoveCam = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canMoveCam = false;
    }

    
}
