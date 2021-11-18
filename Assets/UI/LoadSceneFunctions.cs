using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneFunctions : MonoBehaviour
{
    public AudioClip audioClip;
    //MainUI
    public void StartButtonPressed()
    {
        SoundScript.PlayAudio(audioClip);
        DontDestroyOnLoad(SoundScript.Instance.gameObject);
        SceneManager.LoadScene("StartMenu");
    }

    //StartUI
    public void StartEasyGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(1);
        SceneManager.LoadScene("ButterHunt");
    }

    public void StartMediumGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(2);
        SceneManager.LoadScene("ButterHunt");
    }


    public void StartHardGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        ButterHuntVariables.SetMode(3);
        SceneManager.LoadScene("ButterHunt");
    }

    //Shared
    public void RestarGametPress()
    {
        SoundScript.PlayAudio(audioClip);
        GetComponent<GameManager>().Start();
    }

    public void BackToMain()
    {
        SoundScript.PlayAudio(audioClip);
        SceneManager.LoadScene("MainMenu");
    }


    public void ExitGamePress()
    {
        SoundScript.PlayAudio(audioClip);
        Application.Quit();
    }
}
