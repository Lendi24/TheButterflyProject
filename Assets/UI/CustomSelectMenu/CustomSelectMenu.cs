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
    public Slider preHuntTime, huntTime;

    //public SliderInt startAmountRandom, startAmountGene, roundSpawnAmount, healthAmount;
    //public MinMaxSlider kills;
    //public Toggle resetOnNextGen, keepButterflyAmount, noSafeClick, renderButterBack;

    public DropdownField populationBias;
    public TextField amountOfWhite, amountOfGray, amountOfDark;

    public SliderInt minKills, maxClicks, initButterAmount, roundSpawnAmount, healthAmount;
    public Toggle resetOnNextGen, keepButterflyAmount, invertetNoSafeClick;
    public RadioButtonGroup geneModeRadio, renderModeRadio;
    VisualElement root;

    private bool dominActivated, lastChangeWasByScript;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //Page: Simulation Config
        healthAmount = root.Q<SliderInt>("menu-slider-healthamount"); //Unchanged
        minKills = root.Q<SliderInt>("menu-slider-min-kills");
        minKills.RegisterCallback<ChangeEvent<int>>(OnChangeMinKills);
        maxClicks = root.Q<SliderInt>("menu-slider-max-click");
        maxClicks.RegisterCallback<ChangeEvent<int>>(OnChangeMaxKills);
        invertetNoSafeClick = root.Q<Toggle>("menu-toggle-nosafeclick");
        renderModeRadio = root.Q<RadioButtonGroup>("radio-render-mode");

        //Page: Butterfly Settings
        resetOnNextGen = root.Q<Toggle>("menu-toggle-resetonnextgen"); //Unchanged
        keepButterflyAmount = root.Q<Toggle>("menu-toggle-keepbutterflyamount"); //Unchanged
        roundSpawnAmount = root.Q<SliderInt>("menu-slider-roundspawnamount"); //Unchanged
        geneModeRadio = root.Q<RadioButtonGroup>("radio-gene-mode"); //Unchanged
        geneModeRadio.Q<RadioButton>("none").RegisterCallback<ChangeEvent<bool>>(OnGeneDominChange);

        //Page: Initial Environment
        preHuntTime = root.Q<Slider>("menu-slider-prehunttime"); //Unchanged
        huntTime = root.Q<Slider>("menu-slider-hunttime"); //Unchanged
        initButterAmount = root.Q<SliderInt>("menu-slider-init-butteramount");
        initButterAmount.RegisterCallback<ChangeEvent<int>>(OnInitamntChange);

        populationBias = root.Q<DropdownField>("dropdown-bias");
        populationBias.RegisterCallback<ChangeEvent<string>>(OnDropdownChange, TrickleDown.TrickleDown);

        amountOfWhite = root.Q<TextField>("textf-amount-of-white");
        amountOfWhite.RegisterCallback<InputEvent>(OnInitamntSpecificChange, TrickleDown.TrickleDown);
        amountOfGray = root.Q<TextField>("textf-amount-of-gray");
        amountOfGray.RegisterCallback<InputEvent>(OnInitamntSpecificChange, TrickleDown.TrickleDown);
        amountOfDark = root.Q<TextField>("textf-amount-of-dark");
        amountOfDark.RegisterCallback<InputEvent>(OnInitamntSpecificChange, TrickleDown.TrickleDown);

        //BottomButtons
        
        root.Q<Button>("menu-button-save").clicked += () => {
            //string name = root.Q<TextField>("config-name").value;
            string fillerName = "{Enter config-name here}";

            GetComponent<PopupsUI>().SpawnpopEnterText(
                title: "Saving config",
                fillerText: fillerName,
                buttonRedText: "Cancel",
                elem: GetComponent<UIDocument>().rootVisualElement,
                buttonGreenText: "Save",
                greenButtonAction: (string name) => {
                    Debug.Log("Trying to save with name \""+name+"\"");

                    try
                    {
                        if (name == "" || name == fillerName)
                        {
                            GetComponent<PopupsUI>().SpawnpopInfoRed(
                                title: "Error!",
                                infoText: "Config not saved. Given name is invalid!",
                                buttonRedText: "Cancel",
                                elem: GetComponent<UIDocument>().rootVisualElement,
                                redButtonAction: () => { }
                            );
                        }

                        else
                        {
                            ConfigurationFunctions.SaveToFile(
                                MakeConfObject(name),
                                ConfigurationFunctions.GetValidFilenameFromName(name, 0)
                            );
                        }

                    }
                    catch (System.Exception)
                    {
                        GetComponent<PopupsUI>().SpawnpopInfoRed(
                            title: "Error!",
                            infoText: "Woops! You aren't supposed to see this \\o/\nTheButterflyProject ran into an error it couldn't handle.\nCheck logs for details!",
                            buttonRedText: "Cancel",
                            elem: GetComponent<UIDocument>().rootVisualElement,
                            redButtonAction: () => { }
                        );
                        throw;
                    }
                }
            );
        };

        root.Q<Button>("menu-button-play").clicked += () => {
            CurrentConfig.conf = MakeConfObject("the dev was too lazy to name this object :C");
            SceneManager.LoadScene("ButterHunt");
        };

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


        //Enter Event YO, SEB! FIX THIS!!
        preHuntTime.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        huntTime.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        healthAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        //renderLerp.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        //renderPerlin.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        roundSpawnAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        keepButterflyAmount.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        resetOnNextGen.RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("light").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("none").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("dark").RegisterCallback<PointerEnterEvent>(OnPointerEnterEvent, TrickleDown.TrickleDown);

        //Exit Event
        preHuntTime.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        huntTime.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        healthAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        //renderLerp.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        //renderPerlin.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        roundSpawnAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        keepButterflyAmount.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        resetOnNextGen.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("light").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("none").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);
        geneModeRadio.Q<RadioButton>("dark").RegisterCallback<PointerLeaveEvent>(OnPointerLeaveEvent, TrickleDown.TrickleDown);

        LoadFromMemory();
    }

    /*-------------
     EVENT HANDLERS
     ------------*/
    private void OnChangeMinKills(ChangeEvent<int> evt)
    {
        if (evt.newValue > maxClicks.value && maxClicks.value != 0)
        {
            maxClicks.value = evt.newValue;
        }
    }

    private void OnChangeMaxKills(ChangeEvent<int> evt)
    {
        if (evt.newValue < minKills.value)
        {
            minKills.value = evt.newValue;
        }
    }

    private void OnGeneDominChange(ChangeEvent<bool> evt)
    {
        Label InitInfoText = root.Q<Label>("InitInfoText");

        dominActivated = !evt.newValue;
        if(dominActivated)
        {
            InitInfoText.text = "A dominant butterfly colour has been set!\n" +
                "\"Gray butterflies\" will spawn as the dominant colour,\n" +
                "but still have the same gene-structure as a gray butterfly would have. I.E. \"Aa\".";
        }

        else
        {
            InitInfoText.text = "No dominant butterfly colour has been set.\n" +
                "Butterflies with gene \"Aa\" will be their own colour";
        }
    }

    private void OnInitamntChange(ChangeEvent<int> evt)
    {
        if (populationBias.value == "Fixed" && !lastChangeWasByScript)
        {
            CalculateButterAmount(evt.newValue);
        }

        else
        {
            int whiteButter, grayButter, darkButter;
            int.TryParse(amountOfWhite.value, out whiteButter);
            int.TryParse(amountOfGray.value, out grayButter);
            int.TryParse(amountOfDark.value, out darkButter);
            int total = whiteButter + grayButter + darkButter;

            if (evt.newValue < total)
            {
                populationBias.value = "Fixed";
            }
        }

        lastChangeWasByScript = false;
    }

    private void CalculateButterAmount(int newValue)
    {
        float newVal;
        int amountOfButtertypes = 3; //If dominant genes are 
        //if (dominActivated) { amountOfButtertypes = 2; }

        if (newValue % amountOfButtertypes == 0)
        {
            newVal = (newValue / amountOfButtertypes);
        }

        else
        {
            newVal = (float)System.Math.Ceiling((float)newValue / (float)amountOfButtertypes);
        }

        amountOfWhite.value = newVal.ToString();
        amountOfGray.value = ((float)newValue - 2 * newVal).ToString();
        amountOfDark.value = newVal.ToString();
    }

    private void OnDropdownChange(ChangeEvent<string> evt)
    {
        switch (evt.newValue)
        {
            case "Fixed":
                amountOfDark.label = "Amount Of Dark";
                amountOfGray.label = "Amount Of Gray";
                amountOfWhite.label = "Amount Of White";
                CalculateButterAmount(initButterAmount.value);

                break;

            case "Random":
                amountOfDark.label = "Minimum Amount Of Dark";
                amountOfDark.value = "1";
                amountOfGray.label = "Minimum Amount Of Gray";
                amountOfGray.value = "1";
                amountOfWhite.label = "Minimum Amount Of White";
                amountOfWhite.value = "1";

                break;

            default:
                break;
        }
    }

    private void OnInitamntSpecificChange(InputEvent evt)
    {
        TextField evtCaller = evt.target as TextField;
        int evtCallerValue;

        if (int.TryParse(evtCaller.value, out evtCallerValue))
        {

            int whiteButter, grayButter, darkButter;
            int.TryParse(amountOfWhite.value, out whiteButter);
            int.TryParse(amountOfGray.value, out grayButter);
            int.TryParse(amountOfDark.value, out darkButter);
            int total = whiteButter + grayButter + darkButter;

            if (total > initButterAmount.highValue)
            {
                initButterAmount.value = initButterAmount.highValue;
                evtCaller.value = (initButterAmount.highValue + evtCallerValue 
                    - whiteButter
                    - grayButter
                    - darkButter
                    ).ToString();

            }

            else if (total > initButterAmount.value)
            {
                initButterAmount.value = total;
            }

            else if (total < initButterAmount.value && populationBias.value == "Fixed")
            {
                initButterAmount.value = total;
            }

            lastChangeWasByScript = true;
        }

        else //This is called when failed to convert new value to int
        {
            switch (evt.newData)
            {
                case "":
                    //evtCaller.value = "0";
                    break;

                default:
                    evtCaller.value = evt.previousData;
                    break;
            }

            
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
   
    void SwitchTab(VisualElement page, Button button)
    {
        ResetSettingsMenu();
        page.AddToClassList("Active");
        button.AddToClassList("Active");
    }

    /*
     Other stuff, idk..
     */

    void ResetSettingsMenu()
    {
        initEnvSettingsPage.RemoveFromClassList("Active");
        gameSettingsPage.RemoveFromClassList("Active");
        butterflySettingsPage.RemoveFromClassList("Active");

        initEnvButton.RemoveFromClassList("Active");
        gameSettingsButton.RemoveFromClassList("Active");
        butterflySettingsButton.RemoveFromClassList("Active");
    }
    
   ConfigurationSettings MakeConfObject(string name)
   {
        int detectedRenderMode = 0;

        if (renderModeRadio.Q<RadioButton>("mixed").value)
        {
            detectedRenderMode = 1;
        }

        else if (renderModeRadio.Q<RadioButton>("perlin").value)
        {
            detectedRenderMode = 2;
        }

        int geneMode = 0;

        if (geneModeRadio.Q<RadioButton>("dark").value)
        {
            geneMode = 1;
        }

        else if(geneModeRadio.Q<RadioButton>("light").value)
        {
            geneMode = 2;
        }

        float renderLerp = 1;
        float renderPerlin = 1;

        switch (detectedRenderMode)
        {
            case 0:
                renderPerlin = 0;
                break;

            case 2:
                renderLerp = 0;
                break;

            default:
                break;
        }

        bool renderBack = !(detectedRenderMode == 2);

        return ConfigurationFunctions.MakeConfObject(
            _confName:                  name, 
            _preHuntTime:               preHuntTime.value,
            _huntTime:                  huntTime.value,

            _initAmntOfButter:          initButterAmount.value,
            _initAmntOfWhite:           int.Parse(amountOfWhite.value),
            _initAmntOfGray:            int.Parse(amountOfGray.value),
            _initAmountOfDark:          int.Parse(amountOfDark.value),

            _maximumKills:              maxClicks.value,
            _minimumKills:              minKills.value,
            _butterflyRoundSpawnAmount: roundSpawnAmount.value,
            _healthAmount:              healthAmount.value,
            _resetEverythingOnNextGen:  resetOnNextGen.value,
            _noSafeClick:               !invertetNoSafeClick.value,
            _keepButterAmount:          keepButterflyAmount.value,
            _renderLerp:                renderLerp,
            _renderPerlin:              renderPerlin,
            _renderButterBackground:    renderBack,
            _geneMode:                  geneMode
        );
    }

    private void LoadFromMemory()
    {
        preHuntTime.value                   =        CurrentConfig.conf.preHuntTime;
        huntTime.value                      =        CurrentConfig.conf.huntTime;
        
        initButterAmount.value              =        CurrentConfig.conf.initAmntOfButter;
        amountOfWhite.value                 =        CurrentConfig.conf.initAmntOfWhite.ToString();
        amountOfGray.value                  =        CurrentConfig.conf.initAmntOfGray.ToString();
        amountOfDark.value                  =        CurrentConfig.conf.initAmountOfDark.ToString();
        
        maxClicks.value                     =        CurrentConfig.conf.maximumKills;
        minKills.value                      =        CurrentConfig.conf.minimumKills;
        roundSpawnAmount.value              =        CurrentConfig.conf.butterflyRoundSpawnAmount;
        healthAmount.value                  =        CurrentConfig.conf.healthAmount;
        resetOnNextGen.value                =        CurrentConfig.conf.resetEverythingOnNextGen; 
        invertetNoSafeClick.value           =       !CurrentConfig.conf.noSafeClick;
        keepButterflyAmount.value           =        CurrentConfig.conf.keepButterAmount;

        switch (CurrentConfig.conf.geneMode)
        {
            case 0:
                geneModeRadio.Q<RadioButton>("light").value = true;
                break;

            case 1:
                geneModeRadio.Q<RadioButton>("none").value = true;
                break;

            default: //really Case 2:, but yeah
                geneModeRadio.Q<RadioButton>("dark").value = true;
                break;
        }

        if (CurrentConfig.conf.renderButterBackground)
        {
            if (CurrentConfig.conf.renderPerlin == 0)
            {
                renderModeRadio.Q<RadioButton>("textured").value = true;
            }

            else
            {
                renderModeRadio.Q<RadioButton>("mixed").value = true;
            }
        }

        else
        {
            renderModeRadio.Q<RadioButton>("perlin").value = true;
        }
    }
}
