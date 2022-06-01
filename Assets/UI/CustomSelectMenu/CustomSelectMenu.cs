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

    //public SliderInt startAmountRandom, startAmountGene, roundSpawnAmount, healthAmount;
    //public MinMaxSlider kills;
    //public Toggle resetOnNextGen, keepButterflyAmount, noSafeClick, renderButterBack;

    public DropdownField populationBias;
    public TextField amountOfWhite, amountOfGray, amountOfDark;

    public SliderInt minKills, maxClicks, initButterAmount, roundSpawnAmount, healthAmount;
    public Toggle resetOnNextGen, keepButterflyAmount, invertetNoSafeClick;
    public RadioButtonGroup geneModeRadio, renderModeRadio;
    VisualElement root;

    private bool dominActivated;

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
            string name = root.Q<TextField>("config-name").value;
            ConfigurationFunctions.SaveToFile(MakeConfObject(), name);
        };

        root.Q<Button>("menu-button-load").clicked += () => {
            SceneManager.LoadScene("ConfigExplorer");
        };

        root.Q<Button>("menu-button-play").clicked += () => {
            //GetComponent<LoadSceneFunctions>().StartCustomGame(preHuntTime.value, huntTime.value, geneLength.value, startAmountRandom.value, startAmountGene.value, Mathf.RoundToInt(kills.maxValue), Mathf.RoundToInt(kills.minValue), renderMode.value, roundSpawnAmount.value, healthAmount.value, resetOnNextGen.value, noSafeClick.value, keepButterflyAmount.value, geneMode);
            CurrentConfig.conf = MakeConfObject();
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
        /*
        //SwitchTab(butterflySettingsPage, butterflySettingsButton);
        try
        {
            loadInitValues();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("NoLoadedConfig!");
        }*/
    }

    /*-------------
     EVENT HANDLERS
     ------------*/
    private void OnChangeMinKills(ChangeEvent<int> evt)
    {
        if (evt.newValue < maxClicks.value)
        {
            maxClicks.value = evt.newValue;
        }
    }

    private void OnChangeMaxKills(ChangeEvent<int> evt)
    {
        if (evt.newValue > minKills.value)
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
        float newVal;
        int amountOfButtertypes = 3; //If dominant genes are 
        //if (dominActivated) { amountOfButtertypes = 2; }

        if (evt.newValue % amountOfButtertypes == 0)
        {
            newVal = (evt.newValue / amountOfButtertypes);
        }

        else
        {
            newVal = (float)System.Math.Ceiling((float)evt.newValue / (float)amountOfButtertypes);
        }

        amountOfWhite.value = newVal.ToString(); 
        amountOfGray.value = ((float)evt.newValue - 2 * newVal).ToString();
        amountOfDark.value = newVal.ToString(); 
    }

    private void OnDropdownChange(ChangeEvent<string> evt)
    {
        VisualElement fixedBias = root.Q<VisualElement>("FixedBias");
        switch (evt.newValue)
        {
            case "Fixed":
                fixedBias.visible = true;
                fixedBias.style.display = DisplayStyle.Flex;
                break;

            case "Random":
                fixedBias.visible = false;
                fixedBias.style.display = DisplayStyle.None;
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

            else 
            {
                
            }
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
           _butterflyStartAmountRandom: 5,//startAmountRandom.value,
           _butterflyStartAmountGene: 5,//startAmountGene.value,
           _maximumKills: 5,//Mathf.RoundToInt(kills.maxValue),
           _minimumKills: 5,//Mathf.RoundToInt(kills.minValue),
           _butterflyRoundSpawnAmount: roundSpawnAmount.value,
           _healthAmount: healthAmount.value,
           _resetEverythingOnNextGen: resetOnNextGen.value,
           _noSafeClick: true,//noSafeClick.value,
           _keepButterAmount: keepButterflyAmount.value,
           _renderLerp: renderLerp.value,
           _renderPerlin: renderPerlin.value,
           _renderButterBackground: false,//renderButterBack.value,
           _geneMode: geneMode);
   }
    /*
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
   }*/

}
