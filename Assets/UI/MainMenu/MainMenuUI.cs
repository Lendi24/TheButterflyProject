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

        string[] subtitles =
        {
            "(TheButterflyProject)",
            "01110100 01100010 01110000",
            "(a.k.a. Fjärilsprojektet)",
            "Learn biology!!",
            "Pay attention in class!!",
            "Don't do drugs, kids!",
            "SMÖRFLUGOR!!!",
            "Butterflies, now in 3D!",
            "Powered by: coffee addiction",
            "Check out the cool graph I made :D",
            "These captions are definitely an original idea!",
            "Supports linux! :-D",
            "Supports Android! :-)",
            "Supports Windows!",
            "Does not support IOS",
            "Eww.. iPhone!",
            "It’s not a bug, it's a feature!!",
            "A game with a fancy caption",
        };

        root.Q<Label>("sub").text = subtitles[Random.Range(0, subtitles.Length)];
    }

    void ExitGame()
    {
        Application.Quit();
    }
}