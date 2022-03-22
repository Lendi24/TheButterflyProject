using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneFunctions : MonoBehaviour
{
    public AudioClip audioClip;
    //MainUI
    public void StartButtonPressed()
    {
        SoundScript.PlayAudio(audioClip);
        //SceneManager.LoadScene("StartMenu");ree
        SceneManager.LoadScene("Networking");

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

        CurrentConfig.conf = ConfigurationFunctions.MakeConfObject(
            _preHuntTime: 1f,
            _huntTime: 3f,

            _butterflyStartAmountRandom: 8,
            _butterflyStartAmountGene: 2,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 3,

            _resetEverythingOnNextGen: true,
            _noSafeClick: false,
            _keepButterAmount: false,
            
            _renderLerp: 1,
            _renderPerlin: 0,
            _renderButterBackground: true,

            _geneMode: 2);

        SceneManager.LoadScene("ButterHunt");
    }

    public void StartMediumGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);

        CurrentConfig.conf = ConfigurationFunctions.MakeConfObject(
            _preHuntTime: 1f,
            _huntTime: 4f,

            _butterflyStartAmountRandom: 8,
            _butterflyStartAmountGene: 2,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 2,

            _resetEverythingOnNextGen: true,
            _noSafeClick: true,
            _keepButterAmount: false,

            _renderLerp: 1,
            _renderPerlin: 1,
            _renderButterBackground: true,

            _geneMode: 0);

        SceneManager.LoadScene("ButterHunt");
    }


    public void StartHardGame()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);

        CurrentConfig.conf = ConfigurationFunctions.MakeConfObject(
            _preHuntTime: 1f,
            _huntTime: 5f,

            _butterflyStartAmountRandom: 8,
            _butterflyStartAmountGene: 2,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 3,

            _resetEverythingOnNextGen: true,
            _noSafeClick: true,
            _keepButterAmount: false,

            _renderLerp: 0,
            _renderPerlin: 1,
            _renderButterBackground: false,

            _geneMode: 1);

        SceneManager.LoadScene("ButterHunt");
    }

    public void StartLastGameMode()
    {
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        SoundScript.Instance.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
        SceneManager.LoadScene("ButterHunt");
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
