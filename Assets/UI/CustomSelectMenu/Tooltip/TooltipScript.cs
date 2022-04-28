using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TooltipScript : MonoBehaviour
{
    Vector3 mousePos;
    VisualElement root;
    VisualElement tooltip;
    Label tooltipLabel;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        tooltip = root.Q<VisualElement>("Tooltip");
        tooltipLabel = root.Q<Label>("tooltip-text");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HideTooltip();
    }

    // Update is called once per frame
    void Update()
    {
        if (mousePos != Camera.main.ScreenToWorldPoint(Input.mousePosition))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            root.transform.position = new Vector3(Input.mousePosition.x, -Input.mousePosition.y, 0);
        }
    }

    public void ShowTooltip(string tooltipText)
    {
        tooltip.style.visibility = Visibility.Visible;
        tooltipLabel.text = tooltipText;
    }

    public void HideTooltip()
    {
        Debug.Log("e?");
        tooltip.style.visibility = Visibility.Hidden;
    }
}
