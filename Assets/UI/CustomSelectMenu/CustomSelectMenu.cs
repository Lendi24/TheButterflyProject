using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CustomSelectMenu : MonoBehaviour
{
    public Button timeSettingsButton, gameSettingsButton, butterflySettingsButton;
    public VisualElement timeSettingsPage, gameSettingsPage, butterflySettingsPage;
    public Slider preHuntTime, huntTime;
    public SliderInt geneLength, startAmountRandom, startAmountGene, renderMode, roundSpawnAmount, healthAmount;
    public MinMaxSlider kills;
    public Toggle resetOnNextGen, keepButterflyAmount, noSafeClick;
    public RadioButtonGroup geneModeRadio;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        preHuntTime = root.Q<Slider>("menu-slider-prehunttime");
        huntTime = root.Q<Slider>("menu-slider-hunttime");
        geneLength = root.Q<SliderInt>("menu-slider-genelength");
        startAmountRandom = root.Q<SliderInt>("menu-slider-startamountrandom");
        startAmountGene = root.Q<SliderInt>("menu-slider-startamountgene");
        renderMode = root.Q<SliderInt>("menu-slider-rendermode");
        roundSpawnAmount = root.Q<SliderInt>("menu-slider-roundspawnamount");
        healthAmount = root.Q<SliderInt>("menu-slider-healthamount");
        kills = root.Q<MinMaxSlider>("menu-minmaxslider-kills");
        resetOnNextGen = root.Q<Toggle>("menu-toggle-resetonnextgen");
        keepButterflyAmount = root.Q<Toggle>("menu-toggle-keepbutterflyamount");
        noSafeClick = root.Q<Toggle>("menu-toggle-nosafeclick");
        

        geneModeRadio = root.Q<RadioButtonGroup>("gene-mode");

        //BottomButtons
        root.Q<Button>("menu-button-save").clicked += SaveCustomConfig;
        root.Q<Button>("menu-button-load").clicked += LoadCustomConfig;
        root.Q<Button>("menu-button-play").clicked += PlayCustomConfig;
        root.Q<Button>("menu-button-back").clicked += GetComponent<LoadSceneFunctions>().BackToMain;

        //Tab-pages
        timeSettingsPage = root.Q<VisualElement>("time-settings-page");
        gameSettingsPage = root.Q<VisualElement>("game-settings-page");
        butterflySettingsPage = root.Q<VisualElement>("butterfly-settings-page");

        //Tab-buttons
        timeSettingsButton = root.Q<Button>("time-settings-button");
        gameSettingsButton = root.Q<Button>("game-settings-button");
        butterflySettingsButton = root.Q<Button>("butterfly-settings-button");


        //Tab-buttons logic-binding 
        timeSettingsButton.clicked += ResetSettingsMenu;
        gameSettingsButton.clicked += ResetSettingsMenu;
        butterflySettingsButton.clicked += ResetSettingsMenu;

        timeSettingsButton.clicked += SwitchToTimeSettings;
        gameSettingsButton.clicked += SwitchToGameSettings;
        butterflySettingsButton.clicked += SwitchToButterflySettings;

    }

    void PlayCustomConfig()
    {
        //GetComponent<LoadSceneFunctions>().StartCustomGame(preHuntTime.value, huntTime.value, geneLength.value, startAmountRandom.value, startAmountGene.value, Mathf.RoundToInt(kills.maxValue), Mathf.RoundToInt(kills.minValue), renderMode.value, roundSpawnAmount.value, healthAmount.value, resetOnNextGen.value, noSafeClick.value, keepButterflyAmount.value, geneMode);
        CurrentConfig.conf = MakeConfObject();
        SceneManager.LoadScene("ButterHunt");
    }

    void LoadCustomConfig()
    {
    }

    void SaveCustomConfig()
    {
        ConfigurationFunctions.SaveToFile(MakeConfObject(), "GÖRAN");
    }

    void SwitchToTimeSettings()
    {
        timeSettingsPage.style.display = DisplayStyle.Flex;
        timeSettingsButton.style.backgroundColor = new StyleColor { value = Color.red };
    }

    void SwitchToGameSettings()
    {
        gameSettingsPage.style.display = DisplayStyle.Flex;
        gameSettingsButton.style.backgroundColor = new StyleColor { value = Color.red };
    }

    void SwitchToButterflySettings()
    {
        butterflySettingsPage.style.display = DisplayStyle.Flex;
        butterflySettingsButton.style.backgroundColor = new StyleColor { value = Color.red };
    }

    void ResetSettingsMenu()
    {
        timeSettingsPage.style.display = DisplayStyle.None;
        gameSettingsPage.style.display = DisplayStyle.None;
        butterflySettingsPage.style.display = DisplayStyle.None;

        timeSettingsButton.style.backgroundColor = new StyleColor { value = Color.white };
        gameSettingsButton.style.backgroundColor = new StyleColor { value = Color.white };
        butterflySettingsButton.style.backgroundColor = new StyleColor { value = Color.white };
    }

    ConfigurationSettings MakeConfObject() {
        bool? geneMode;

        if (geneModeRadio.Q<RadioButton>("light").value)
        {
            geneMode = false;
        }

        else if (geneModeRadio.Q<RadioButton>("dark").value)
        {
            geneMode = true;
        }

        else
        {
            geneMode = null;
        }

        return ConfigurationFunctions.MakeConfObject(
            _preHuntTime:                   preHuntTime.value,
            _huntTime:                      huntTime.value,
            _butterflyGeneLength:           geneLength.value,
            _butterflyStartAmountRandom:    startAmountRandom.value,
            _butterflyStartAmountGene:      startAmountGene.value,
            _maximumKills:                  Mathf.RoundToInt(kills.maxValue),
            _minimumKills:                  Mathf.RoundToInt(kills.minValue),
            _butterflyRoundSpawnAmount:     roundSpawnAmount.value,
            _healthAmount:                  healthAmount.value,
            _resetEverythingOnNextGen:      resetOnNextGen.value,
            _noSafeClick:                   noSafeClick.value,
            _keepButterAmount:              keepButterflyAmount.value,

            //PlaceholderRenderVariables
            _renderLerp: 1.0f,
            _renderPerlin: 1.0f,
            _renderButterBackground: true,

            _geneMode:                      geneMode);

    }

}
