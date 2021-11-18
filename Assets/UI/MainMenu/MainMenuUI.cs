using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject soundObject;
    public Button startButton;
    public Button loadButton;
    public Button settingsButton;
    public Button creditsButton;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement; 
        startButton = root.Q<Button>("menu-button-start");
        loadButton = root.Q<Button>("menu-button-load");
        settingsButton = root.Q<Button>("menu-button-settings");
        creditsButton = root.Q<Button>("menu-button-credits");

        startButton.clicked += GetComponent<LoadSceneFunctions>().StartButtonPressed;
        loadButton.clicked += GetComponent<LoadSceneFunctions>().LoadButtonPressed;
        if (GameObject.Find("ClickSound") == null)
        {
            GameObject newSoundObject = Instantiate<GameObject>(soundObject);
            newSoundObject.transform.parent = transform.parent;
            newSoundObject.name = "ClickSound";
        }
    }
}