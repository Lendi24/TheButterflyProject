using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFunctions : MonoBehaviour
{
    //MainUI
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("StartMenu");
    }

    //StartUI
    public void StartEasyGame()
    {
        ButterHuntVariables.SetMode(1);
        SceneManager.LoadScene("ButterHunt");
    }

    public void StartMediumGame()
    {
        ButterHuntVariables.SetMode(2);
        SceneManager.LoadScene("ButterHunt");
    }


    public void StartHardGame()
    {
        ButterHuntVariables.SetMode(3);
        SceneManager.LoadScene("ButterHunt");
    }

    //Shared
    public void RestarGametPress()
    {
        GetComponent<GameManager>().Start();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ExitGamePress()
    {
        Application.Quit();
    }
}
