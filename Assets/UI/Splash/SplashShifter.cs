using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SplashShifter : MonoBehaviour
{
    [SerializeField]
    Button restart, main, exit, graph;
    Label generation, score;

    private void Start()
    {
        //No touchy!! This code makes GameManager's Start() to run twice!
        //GetComponent<LoadSceneFunctions>().RestartGamePress();
    }

    private void AssignBittons()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;


        try
        {
            restart = root.Q<Button>("restart-game");
            restart.clicked += GetComponent<LoadSceneFunctions>().StartLastGameMode;
        }

        catch (System.Exception)
        {
            //Debug.LogError(e);
        }


        try
        {
            main = root.Q<Button>("main-menu");
            main.clicked += GetComponent<LoadSceneFunctions>().BackToMain;
        }

        catch (System.Exception)
        {
            //Debug.LogError(e);
        }


        try
        {
            graph = root.Q<Button>("graph-stats");
            graph.clicked += GetComponent<LoadSceneFunctions>().ToGraph;
        }

        catch (System.Exception)
        {
            //Debug.LogError(e);
        }

        try
        {
            score = root.Q<Label>("score");
            score.text = GetComponent<GameManager>().GetScore().ToString();
        }

        catch (System.Exception)
        {
            //Debug.LogError(e);
        }

        try
        {
            generation = root.Q<Label>("generation");
            generation.text = "Generation " + GetComponent<GameManager>().GetRounds();
        }

        catch (System.Exception)
        {
            //Debug.LogError(e);
        }

        //exit = root.Q<Button>("exit-game");

        //exit.clicked += GetComponent<LoadSceneFunctions>().ExitGamePress;
    }

    public void ShowSplash(float time, VisualTreeAsset splash)
    {
        GetComponent<UIDocument>().visualTreeAsset = splash; 
        GetComponent<UIDocument>().enabled = true; 

        if (time > 0)
        {
            //yield return new WaitForSeconds(time);
        }

        AssignBittons();


    }

    public void HideSplash()
    {
        GetComponent<UIDocument>().enabled = false;
    }


}
