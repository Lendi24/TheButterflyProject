using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CustomSelectMenu : MonoBehaviour
{
    public Slider preHuntTime, huntTime;
    public SliderInt geneLength, startAmountRandom, startAmountGene, renderMode, roundSpawnAmount, healthAmount;
    public MinMaxSlider kills;
    public Toggle resetOnNextGen, keepButterflyAmount, noSafeClick;
    public Button playButton;
    public Button backButton;
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
        playButton = root.Q<Button>("menu-button-play");
        backButton = root.Q<Button>("menu-button-back");
        
        playButton.clicked += CreateCustomGame;
        backButton.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
    }

    void CreateCustomGame()
    {
        GetComponent<LoadSceneFunctions>().StartCustomGame(preHuntTime.value, huntTime.value, geneLength.value, startAmountRandom.value, startAmountGene.value, Mathf.RoundToInt(kills.maxValue), Mathf.RoundToInt(kills.minValue), renderMode.value, roundSpawnAmount.value, healthAmount.value, resetOnNextGen.value, noSafeClick.value, keepButterflyAmount.value);
    }

}
