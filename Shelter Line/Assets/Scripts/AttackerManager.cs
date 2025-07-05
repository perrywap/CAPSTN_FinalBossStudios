using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackerManager : MonoBehaviour
{
    #region VARIABLES

    [SerializeField]
    private float _preparationTime;

    #endregion

    #region UNITY METHODS
    private void Start()
    {
        StartCoroutine(StartPreparationTime());
        

    }

    #endregion

    #region COROUTINES
    private IEnumerator StartPreparationTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        _preparationTime--;
    }
    #endregion
}
