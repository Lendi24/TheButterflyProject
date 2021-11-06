using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button settingsButton;
    public Button creditsButton;

    public StyleSheet ussSmallScreens;
    public StyleSheet ussBigScreens;

    VisualElement root;

    private void Update()
    {
        if (Screen.height < 420 || Screen.width < 470)
        {
            root.styleSheets.Add(ussSmallScreens);
            root.styleSheets.Remove(ussBigScreens);
            Debug.Log("Small screen");
        }

        else
        {
            root.styleSheets.Add(ussBigScreens);
            root.styleSheets.Remove(ussSmallScreens);
            Debug.Log("Big screen");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("menu-button-start");
        loadButton = root.Q<Button>("menu-button-load");
        settingsButton = root.Q<Button>("menu-button-settings");
        creditsButton = root.Q<Button>("menu-button-credits");

        startButton.clicked += StartButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("StartMenu");
    }
}