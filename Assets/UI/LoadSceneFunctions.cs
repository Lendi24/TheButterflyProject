using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneFunctions : MonoBehaviour
{
    //MainUI
    public void StartButtonPressed()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(SoundScript.Instance.gameObject);
        SceneManager.LoadScene("StartMenu");
    }

    //StartUI
    public void StartEasyGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(1);
        SceneManager.LoadScene("ButterHunt");
    }

    public void StartMediumGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(2);
        SceneManager.LoadScene("ButterHunt");
    }


    public void StartHardGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(3);
        SceneManager.LoadScene("ButterHunt");
    }

    //Shared
    public void RestarGametPress()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().Play();
        GetComponent<GameManager>().Start();
    }

    public void BackToMain()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }


    public void ExitGamePress()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}
