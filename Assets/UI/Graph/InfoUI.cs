using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoUI : MonoBehaviour
{
    public VisualElement roundsCont;
    public VisualElement graphStatCont;
    public VisualElement[] graphStats;
    VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        roundsCont = root.Q<VisualElement>("value-container");
        graphStatCont = root.Q<VisualElement>("color-stat-container");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGraphStats(int colorAmount, string[][] stats, int round)
    {
        roundsCont.Q<Label>("value-count").text = "" + round;
        graphStats = new VisualElement[colorAmount];
        for(int i = 0; i < graphStats.Length; i++)
        {
            graphStats[i] = new VisualElement();
            graphStats[i].name = "color-value-container";
            Label graphValueName = new Label();
            graphValueName.name = "color-value-name";
            graphValueName.text = stats[i][0];
            graphStats[i].Add(graphValueName);
            Label graphValueCount = new Label();
            graphValueCount.name = "color-value-count";
            graphValueCount.text = stats[i][1];
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
        root.transform.position = new Vector3(Input.mousePosition.x, -Input.mousePosition.y, 0);
    }
}
