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

    float expectedDpi;
    bool changeRes;

    VisualElement root;


    // Start is called before the first frame update
    void Start()
    {
        changeRes = true;
        expectedDpi = GetComponent<UIDocument>().panelSettings.referenceDpi;
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("menu-button-start");
        loadButton = root.Q<Button>("menu-button-load");
        settingsButton = root.Q<Button>("menu-button-settings");
        creditsButton = root.Q<Button>("menu-button-credits");

        startButton.clicked += StartButtonPressed;
    }

    private void Update()
    {

        if (Screen.height / (Screen.dpi / expectedDpi) < 420 || Screen.width / (Screen.dpi / expectedDpi) < 470)
        {
            root.styleSheets.Add(ussSmallScreens);
            root.styleSheets.Remove(ussBigScreens);
            changeRes = false;
        }

        else
        {
            root.styleSheets.Add(ussBigScreens);
            root.styleSheets.Remove(ussSmallScreens);
            changeRes = false;
        }
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("StartMenu");
    }
}