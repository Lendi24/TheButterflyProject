using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneFunctions : MonoBehaviour
{
    public AudioClip audioClip;
    //MainUI
    public void StartButtonPressed()
    {
        SoundScript.PlayAudio(audioClip);
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadButtonPressed()
    {
        SoundScript.PlayAudio(audioClip);
        SceneManager.LoadScene("CustomSelectMenu");
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

    public void StartLastGameMode()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        SceneManager.LoadScene("ButterHunt");
    }

    //CustomSelectUI


    public void StartCustomGame(
        float _preHuntTime, 
        float _huntTime, 
        int _butterflyGeneLength, 
        int _butterflyStartAmountRandom, 
        int _butterflyStartAmountGene, 
        int _maximumKills, 
        int _minimumKills, 
        int _butterflyRenderMode, 
        int _butterflyRoundSpawnAmount, 
        int _healthAmount, 
        bool _resetEverythingOnNextGen, 
        bool _noSafeClick, 
        bool _keepButterAmount, 
        bool? _geneMode)
    {

        /*
        ButterHuntVariables.SetCustom(
            _preHuntTime, 
            _huntTime, 
            _butterflyGeneLength, 
            _butterflyStartAmountRandom, 
            _butterflyStartAmountGene, 
            _maximumKills, 
            _minimumKills, 
            _butterflyRenderMode, 
            _butterflyRoundSpawnAmount, 
            _healthAmount, 
            _resetEverythingOnNextGen, 
            _noSafeClick, 
            _keepButterAmount);
        SceneManager.LoadScene("ButterHunt");*/
    }

    //Shared
    
    public void RestartGamePress()
    {
        SoundScript.PlayAudio(audioClip);
        GetComponent<GameManager>().Start();
    }

    public void BackToMain()
    {
        SoundScript.PlayAudio(audioClip);
        SceneManager.LoadScene("MainMenu");
    }

    public void ToGraph()
    {
        SoundScript.PlayAudio(audioClip);
        SceneManager.LoadScene("GraphScene");
    }


    public void ExitGamePress()
    {
        SoundScript.PlayAudio(audioClip);
        Application.Quit();
    }
}
