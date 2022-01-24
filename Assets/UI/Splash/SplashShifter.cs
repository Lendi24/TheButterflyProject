using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SplashShifter : MonoBehaviour
{
    [SerializeField]
    Button restart, main, exit;
    Label score;

    private void Start()
    {
        GetComponent<LoadSceneFunctions>().RestarGametPress();
    }
     
    private void AssignBittons()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        restart = root.Q<Button>("restart-game");
        main = root.Q<Button>("main-menu");
        score = root.Q<Label>("score");
        //exit = root.Q<Button>("exit-game");

        score.text = GetComponent<GameManager>().GetScore().ToString();
        restart.clicked += GetComponent<LoadSceneFunctions>().RestarGametPress;
        main.clicked += GetComponent<LoadSceneFunctions>().BackToMain; 
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

        try
        {
            AssignBittons();
        }

        catch (System.Exception e)
        {
            //Debug.LogError(e);
        }
    }

    public void HideSplash()
    {
        GetComponent<UIDocument>().enabled = false;
    }

    
}
