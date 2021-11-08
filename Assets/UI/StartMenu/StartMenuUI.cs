using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public Button easyModeBtn;
    public Button mediumModeBtn;
    public Button hardModeBtn;
    public Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        easyModeBtn = root.Q<Button>("menu-button-easy");
        mediumModeBtn = root.Q<Button>("menu-button-medium");
        hardModeBtn = root.Q<Button>("menu-button-hard");
        backBtn = root.Q<Button>("menu-button-back");

        easyModeBtn.clicked += StartEasyGame;
        mediumModeBtn.clicked += StartMediumGame;
        hardModeBtn.clicked += StartHardGame;

        backBtn.clicked += BackToMain;

    }

    void StartEasyGame()
    {
        ButterHuntVariables.SetMode(1);
        SceneManager.LoadScene("ButterHunt");
    }

    void StartMediumGame()
    {
        ButterHuntVariables.SetMode(2);
        SceneManager.LoadScene("ButterHunt");
    }


    void StartHardGame()
    {
        ButterHuntVariables.SetMode(3);
        SceneManager.LoadScene("ButterHunt");
    }


    void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
