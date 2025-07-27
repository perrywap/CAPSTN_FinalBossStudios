using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchantCard : MonoBehaviour, IPointerClickHandler
{
    public Transform content;
    public int price;
    public Text priceTxt;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCard();
        priceTxt.text = price.ToString();
    }

    public virtual void InitializeCard()
    {

    }

    public virtual void OnCardClicked()
    {
        Debug.Log("Bought card");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //this.gameObject.GetComponentInChildren<UpgradeCard>().Activate();
    }
}
