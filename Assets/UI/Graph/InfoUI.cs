using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoUI : MonoBehaviour
{
    public VisualElement infoBox;
    public Label roundText;
    public VisualElement graphStatCont;
    public VisualElement[] graphStats;
    VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        infoBox = root.Q<VisualElement>("Infobox");
        roundText = root.Q<Label>("round-text");
        graphStatCont = root.Q<VisualElement>("color-stat-container");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGraphStats(int colorAmount, string[][] stats, int round, Color[] colors)
    {
        roundText.text = "Generation " + round;
        graphStats = new VisualElement[colorAmount];
        for(int i = graphStats.Length-1; i > -1; i--)
        {
            graphStats[i] = new VisualElement();
            graphStats[i].name = "color-value-container";
            Label graphValueName = new Label();
            graphValueName.name = "color-value-name";
            graphValueName.text = stats[i][0];
            graphValueName.style.color = colors[i];
            graphValueName.style.unityFontStyleAndWeight = FontStyle.Bold;
            graphValueName.style.unityTextOutlineWidth = 0.3f;
            graphValueName.style.unityTextOutlineColor = Color.black;
            graphStats[i].Add(graphValueName);
            Label graphValueCount = new Label();
            graphValueCount.name = "color-value-count";
            graphValueCount.text = stats[i][1];
            graphValueCount.style.color = colors[i];
            graphValueCount.style.unityFontStyleAndWeight = FontStyle.Bold;
            graphValueCount.style.unityTextOutlineWidth = 0.3f;
            graphValueCount.style.unityTextOutlineColor = Color.black;
            graphStats[i].Add(graphValueCount);

            graphStatCont.Add(graphStats[i]);
        }
    }

    public void DestroyStats()
    {
        for(int i = 0; i < graphStats.Length; i++)
        {
            graphStatCont.Remove(graphStats[i]);
        }
    }

    public void UpdatePosition()
    {
        float newX = 0;
        if(Input.mousePosition.x - infoBox.resolvedStyle.width/2 + infoBox.resolvedStyle.width > Screen.width)
        {
            newX = Screen.width - infoBox.resolvedStyle.width;
        }
        else if(Input.mousePosition.x - infoBox.resolvedStyle.width/2 < 0)
        {
            newX = 0;
        }
        else
        {
            newX = Input.mousePosition.x - infoBox.resolvedStyle.width/2;
        }

        float newY = 0;
        if (Input.mousePosition.y + 30 + infoBox.resolvedStyle.height > Screen.height)
        {
            newY = Screen.height - infoBox.resolvedStyle.height;
        }
        else if (Input.mousePosition.y + 30 < 0)
        {
            newY = 0;
        }
        else
        {
            newY = Input.mousePosition.y + 30;
        }
        root.transform.position = new Vector3(newX, -newY, 0);
    }
}
