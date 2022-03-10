using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GraphUI : MonoBehaviour
{
    public Button switchGraph;
    public Button restart;
    public Button menu;

    public GameObject graph;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        switchGraph = root.Q<Button>("switch-graph");
        restart = root.Q<Button>("restart-game");
        menu = root.Q<Button>("main-menu");

        switchGraph.clicked += graph.GetComponent<GraphScript>().SwitchGraph;
        restart.clicked += GetComponent<LoadSceneFunctions>().StartLastGameMode;
        menu.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
    }
}
