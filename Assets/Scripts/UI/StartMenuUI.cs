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

        easyModeBtn.clicked += StartGame;
        mediumModeBtn.clicked += StartGame;
        hardModeBtn.clicked += StartGame;

        backBtn.clicked += BackToMain;

    }

    void StartGame()
    {
        SceneManager.LoadScene("ButterHunt");
    }

    void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
