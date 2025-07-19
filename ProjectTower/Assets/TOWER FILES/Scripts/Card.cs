using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("CARD UI REFERENCES")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text manaCostText;
    [SerializeField] private Text healthText;
    [SerializeField] private Image portrait;
    [SerializeField] private Image TypeIcon;
    [SerializeField] private Sprite[] TypeIcons;
    [SerializeField] private GameObject summonPrefab;

    [SerializeField] private int popValue;

    private float yPos;
    private Vector3 originalPos;
    private CanvasGroup canvasGroup;

    public GameObject SummonPrefab {  get { return summonPrefab; } set { summonPrefab = value; } }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalPos = transform.position;

        if(summonPrefab != null )
        {
            //nameText.text = summonPrefab.GetComponent<Unit>().UnitName;
            manaCostText.text = summonPrefab.GetComponent<Unit>().ManaCost.ToString();
            healthText.text = summonPrefab.GetComponent<Unit>().Hp.ToString();
            portrait.sprite = summonPrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        yPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y + popValue, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPos = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = originalPos;
    }
}
