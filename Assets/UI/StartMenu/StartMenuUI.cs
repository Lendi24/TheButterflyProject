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

        easyModeBtn.clicked += GetComponent<LoadSceneFunctions>().StartEasyGame;
        mediumModeBtn.clicked += GetComponent<LoadSceneFunctions>().StartMediumGame;
        hardModeBtn.clicked += GetComponent<LoadSceneFunctions>().StartHardGame;

        backBtn.clicked += GetComponent<LoadSceneFunctions>().BackToMain;

    }
}
