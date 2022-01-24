using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GraphUI : MonoBehaviour
{
    public Button restart;
    public Button menu;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        restart = root.Q<Button>("restart-game");
        menu = root.Q<Button>("main-menu");

        restart.clicked += GetComponent<LoadSceneFunctions>().StartLastGameMode;
        menu.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
    }
}
