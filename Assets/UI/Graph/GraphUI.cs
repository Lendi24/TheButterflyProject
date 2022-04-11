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
    public Label graphText;

    public GameObject graph;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        switchGraph = root.Q<Button>("switch-graph");
        restart = root.Q<Button>("restart-game");
        menu = root.Q<Button>("main-menu");
        graphText = root.Q<Label>("graph-text");

        switchGraph.clicked += graph.GetComponent<GraphBoardScript>().SwitchGraph;
        restart.clicked += GetComponent<LoadSceneFunctions>().StartLastGameMode;
        menu.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
    }

    public void ChangeText(string text)
    {
        graphText.text = text;
    }
}
