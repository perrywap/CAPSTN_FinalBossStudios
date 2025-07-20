using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCore : MonoBehaviour
{
    // THIS PORTAL CORE WILL ONLY NEED
    // ONE (1) UNIT TO PASS THRU TO
    // WIN THE CURRENT LEVEL

    public void GameEnd()
    {
        GameManager.Instance.winPanel.SetActive(true);
        GameManager.Instance.isGameFinished = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            GameEnd();
        }
    }
}
