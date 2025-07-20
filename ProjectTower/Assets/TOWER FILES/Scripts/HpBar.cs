using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private bool showHpBar;
    [SerializeField] private float popUpTime;

    private float currentHealth;
    private float maxHealth;
    private Coroutine coroutine;


    private void Start()
    {
        if(this.GetComponentInParent<Unit>() != null)
        {
            Debug.Log("Got unit health");
            maxHealth = this.GetComponentInParent<Unit>().Hp;
        }

        if(this.GetComponentInParent<Tower>() != null)
        {
            Debug.Log("Got unit tower");
            maxHealth = this.GetComponentInParent<Tower>().Hp;
        }
    }

    private void Update()
    {
        if(showHpBar)
        {
            if (this.GetComponentInParent<Unit>() != null)
            {
                Debug.Log("Got unit health");
                currentHealth = this.GetComponentInParent<Unit>().Hp;
            }

            if (this.GetComponentInParent<Tower>() != null)
            {
                Debug.Log("Got unit tower");
                currentHealth = this.GetComponentInParent<Tower>().Hp;
            }

            hpBar.fillAmount = currentHealth / maxHealth;
        }

        hpBar.gameObject.SetActive(showHpBar ? true : false);
    }

    public void PopHpBar()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        showHpBar = true;
        yield return new WaitForSeconds(popUpTime);
        showHpBar = false;
    }
}
