using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Credits : MonoBehaviour
{
    public Button backButton;
    VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("button-back");

        backButton.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
