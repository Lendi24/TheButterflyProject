using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button settingsButton;
    public Button creditsButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("menu-button-start");
        loadButton = root.Q<Button>("menu-button-load");
        settingsButton = root.Q<Button>("menu-button-settings");
        creditsButton = root.Q<Button>("menu-button-credits");

        startButton.clicked += StartButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("ButterHunt");
    }
}
