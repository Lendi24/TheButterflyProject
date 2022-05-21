using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject soundObject;
    public Button startButton;
    public Button loadButton;
    public Button creditsButton;
    public Button exitButton;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        root = GetComponent<UIDocument>().rootVisualElement; 
        startButton = root.Q<Button>("menu-button-start");
        loadButton = root.Q<Button>("menu-button-load");
        creditsButton = root.Q<Button>("menu-button-credits");
        exitButton = root.Q<Button>("menu-button-exit");

        startButton.clicked += GetComponent<LoadSceneFunctions>().StartButtonPressed;
        loadButton.clicked += GetComponent<LoadSceneFunctions>().LoadButtonPressed;
        creditsButton.clicked += GetComponent<LoadSceneFunctions>().CreditsButtonPressed;
        //credits
        exitButton.clicked += Application.Quit;

        if (GameObject.Find("ClickSound") == null)
        {
            GameObject newSoundObject = Instantiate<GameObject>(soundObject);
            newSoundObject.transform.parent = transform.parent;
            newSoundObject.name = "ClickSound";
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}