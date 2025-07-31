using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private KeyCode toggleKey = KeyCode.T; 

    private bool isVisible = false;

    void Start()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTooltip();
        }
    }

    public void ToggleTooltip()
    {
        if (tooltipPanel != null)
        {
            isVisible = !isVisible;
            tooltipPanel.SetActive(isVisible);
        }
    }
}
