using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CustomSelectMenu : MonoBehaviour
{
    [SerializeField]
    public UIDocument tooltip;
    public Button initEnvButton, gameSettingsButton, butterflySettingsButton;
    public VisualElement initEnvSettingsPage, gameSettingsPage, butterflySettingsPage;
    public Slider preHuntTime, huntTime, renderPerlin, renderLerp;
    public SliderInt startAmountRandom, startAmountGene, roundSpawnAmount, healthAmount;
    public MinMaxSlider kills;
    public Toggle resetOnNextGen, keepButterflyAmount, noSafeClick, renderButterBack;
    public RadioButtonGroup geneModeRadio;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //Page: Time
        preHuntTime = root.Q<Slider>("menu-slider-prehunttime");
        huntTime = root.Q<Slider>("menu-slider-hunttime");

        //Page: Game
        healthAmount = root.Q<SliderInt>("menu-slider-healthamount");
        kills = root.Q<MinMaxSlider>("menu-minmaxslider-kills");
        noSafeClick = root.Q<Toggle>("menu-toggle-nosafeclick");
        renderLerp = root.Q<Slider>("menu-slider-lerp");
        renderPerlin = root.Q<Slider>("menu-slider-perlin");

        //Page: ButterSettings
        startAmountRandom = root.Q<SliderInt>("menu-slider-startamountrandom");
        startAmountGene = root.Q<SliderInt>("menu-slider-startamountgene");
        roundSpawnAmount = root.Q<SliderInt>("menu-slider-roundspawnamount");
        keepButterflyAmount = root.Q<Toggle>("menu-toggle-keepbutterflyamount");
        resetOnNextGen = root.Q<Toggle>("menu-toggle-resetonnextgen");
        //geneLength = root.Q<SliderInt>("menu-slider-genelength");
        renderButterBack = root.Q<Toggle>("menu-render-butter-back");
        geneModeRadio = root.Q<RadioButtonGroup>("gene-mode");

        //BottomButtons
        root.Q<Button>("menu-button-save").clicked += SaveCustomConfig;
        root.Q<Button>("menu-button-load").clicked += LoadCustomConfig;
        root.Q<Button>("menu-button-play").clicked += PlayCustomConfig;
        root.Q<Button>("menu-button-back").clicked += GetComponent<LoadSceneFunctions>().BackToMain;

        //Tab-pages
        initEnvSettingsPage = root.Q<VisualElement>("initial-env-page");
        gameSettingsPage = root.Q<VisualElement>("game-settings-page");
        butterflySettingsPage = root.Q<VisualElement>("butterfly-settings-page");

        //Tab-buttons
        initEnvButton = root.Q<Button>("initial-env-button");
        gameSettingsButton = root.Q<Button>("game-settings-button");
        butterflySettingsButton = root.Q<Button>("butterfly-settings-button");

        //Tab-buttons logic-binding 
        initEnvButton.clicked += () => { SwitchTab(initEnvSettingsPage, initEnvButton); };
        gameSettingsButton.clicked += () => { SwitchTab(gameSettingsPage, gameSettingsButton); };
        butterflySettingsButton.clicked += () => { SwitchTab(butterflySettingsPage, butterflySettingsButton); };
        SwitchTab(butterflySettingsPage, butterflySettingsButton);

        //Enter Event
        preHuntTime.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        huntTime.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        healthAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        kills.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        noSafeClick.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        renderLerp.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        renderPerlin.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        startAmountRandom.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        startAmountGene.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        roundSpawnAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        keepButterflyAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        resetOnNextGen.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        renderButterBack.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("light").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("none").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("dark").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        //Exit Event
        preHuntTime.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        huntTime.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        healthAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        kills.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        noSafeClick.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        renderLerp.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        renderPerlin.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        startAmountRandom.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        startAmountGene.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        roundSpawnAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        keepButterflyAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        resetOnNextGen.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        renderButterBack.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("light").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("none").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("dark").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        //SwitchTab(butterflySettingsPage, butterflySettingsButton);
        try
        {
            loadInitValues();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("NoLoadedConfig!");
        }
    }

    private void OnPointerEnterEvent(PointerEnterEvent evt)
    {
        if (evt.target as SliderInt != null)
        {
            tooltip.GetComponent<TooltipScript>().ShowTooltip((evt.target as SliderInt).tooltip);
        }
        else if (evt.target as Slider != null)
        {
            tooltip.GetComponent<TooltipScript>().ShowTooltip((evt.target as Slider).tooltip);
        }
        else if (evt.target as MinMaxSlider != null)
        {
            tooltip.GetComponent<TooltipScript>().ShowTooltip((evt.target as MinMaxSlider).tooltip);
        }
        else if (evt.target as Toggle != null)
        {
            tooltip.GetComponent<TooltipScript>().ShowTooltip((evt.target as Toggle).tooltip);
        }
        else if (evt.target as RadioButton != null)
        {
            tooltip.GetComponent<TooltipScript>().ShowTooltip((evt.target as RadioButton).tooltip);
        }
    }

    private void OnPointerLeaveEvent(PointerLeaveEvent evt)
    {
        tooltip.GetComponent<TooltipScript>().HideTooltip();
    }


    void PlayCustomConfig()
    {
        //GetComponent<LoadSceneFunctions>().StartCustomGame(preHuntTime.value, huntTime.value, geneLength.value, startAmountRandom.value, startAmountGene.value, Mathf.RoundToInt(kills.maxValue), Mathf.RoundToInt(kills.minValue), renderMode.value, roundSpawnAmount.value, healthAmount.value, resetOnNextGen.value, noSafeClick.value, keepButterflyAmount.value, geneMode);
        CurrentConfig.conf = MakeConfObject();
        SceneManager.LoadScene("ButterHunt");
    }

    void LoadCustomConfig()
    {
        SceneManager.LoadScene("ConfigExplorer");
    }

    void SaveCustomConfig()
    {
        string name = root.Q<TextField>("config-name").value;
        ConfigurationFunctions.SaveToFile(MakeConfObject(), name);
    }

    void SwitchTab(VisualElement page, Button button)
    {
        ResetSettingsMenu();
        page.AddToClassList("Active");
        button.AddToClassList("Active");
    }

    void ResetSettingsMenu()
    {
        initEnvSettingsPage.RemoveFromClassList("Active");
        gameSettingsPage.RemoveFromClassList("Active");
        butterflySettingsPage.RemoveFromClassList("Active");

        initEnvButton.RemoveFromClassList("Active");
        gameSettingsButton.RemoveFromClassList("Active");
        butterflySettingsButton.RemoveFromClassList("Active");
        /*timeSettingsPage.style.display = DisplayStyle.None;
        gameSettingsPage.style.display = DisplayStyle.None;
        butterflySettingsPage.style.display = DisplayStyle.None;

        timeSettingsButton.style.backgroundColor = new StyleColor { value = Color.white };
        gameSettingsButton.style.backgroundColor = new StyleColor { value = Color.white };
        butterflySettingsButton.style.backgroundColor = new StyleColor { value = Color.white };*/
    }

    ConfigurationSettings MakeConfObject()
    {
        int geneMode;

        if (geneModeRadio.Q<RadioButton>("light").value)
        {
            geneMode = 2;
        }

        else if (geneModeRadio.Q<RadioButton>("dark").value)
        {
            geneMode = 1;
        }

        else
        {
            geneMode = 0;
        }

        return ConfigurationFunctions.MakeConfObject(
            _confName: root.Q<TextField>("config-name").value,
            _preHuntTime: preHuntTime.value,
            _huntTime: huntTime.value,
            //_butterflyGeneLength:           geneLength.value,
            _butterflyStartAmountRandom: startAmountRandom.value,
            _butterflyStartAmountGene: startAmountGene.value,
            _maximumKills: Mathf.RoundToInt(kills.maxValue),
            _minimumKills: Mathf.RoundToInt(kills.minValue),
            _butterflyRoundSpawnAmount: roundSpawnAmount.value,
            _healthAmount: healthAmount.value,
            _resetEverythingOnNextGen: resetOnNextGen.value,
            _noSafeClick: noSafeClick.value,
            _keepButterAmount: keepButterflyAmount.value,
            _renderLerp: renderLerp.value,
            _renderPerlin: renderPerlin.value,
            _renderButterBackground: renderButterBack.value,
            _geneMode: geneMode);
    }

    void loadInitValues()
    {
        preHuntTime.value = CurrentConfig.conf.preHuntTime;
        huntTime.value = CurrentConfig.conf.huntTime;
        //geneLength.value = CurrentConfig.conf.butterflyGeneLength;
        startAmountRandom.value = CurrentConfig.conf.butterflyStartAmountRandom;
        startAmountGene.value = CurrentConfig.conf.butterflyStartAmountGene;
        kills.maxValue = CurrentConfig.conf.maximumKills;
        kills.minValue = CurrentConfig.conf.minimumKills;
        roundSpawnAmount.value = CurrentConfig.conf.butterflyRoundSpawnAmount;
        healthAmount.value = CurrentConfig.conf.healthAmount;
        resetOnNextGen.value = CurrentConfig.conf.resetEverythingOnNextGen;
        noSafeClick.value = CurrentConfig.conf.noSafeClick;
        keepButterflyAmount.value = CurrentConfig.conf.keepButterAmount;
        renderLerp.value = CurrentConfig.conf.renderLerp;
        renderPerlin.value = CurrentConfig.conf.renderPerlin;
        renderButterBack.value = CurrentConfig.conf.renderButterBackground;


        switch (CurrentConfig.conf.geneMode)
        {
            case 0:
                geneModeRadio.Q<RadioButton>("light").value = false;
                geneModeRadio.Q<RadioButton>("none").value = true;
                geneModeRadio.Q<RadioButton>("dark").value = false;

                break;

            case 1:
                geneModeRadio.Q<RadioButton>("light").value = false;
                geneModeRadio.Q<RadioButton>("none").value = false;
                geneModeRadio.Q<RadioButton>("dark").value = true;
                break;

            case 2:
                geneModeRadio.Q<RadioButton>("light").value = true;
                geneModeRadio.Q<RadioButton>("none").value = false;
                geneModeRadio.Q<RadioButton>("dark").value = false;
                break;
        }
    }
}
