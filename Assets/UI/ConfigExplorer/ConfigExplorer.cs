using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ConfigExplorer : MonoBehaviour
{
    VisualElement cards;

    // Start is called before the first frame update
    void Start()
    {
        cards = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Cards");
        //CreateCard("Råger");
        GetConfigFiles();
        //cards.RegisterCallback<PointerDownEvent, string>(ButtonInListEditPressed, "te");
    }


    void GetConfigFiles()
    {
        string[] files = ConfigurationFunctions.GetConfigFiles();

        for (int i = 0; i < files.Length; i++)
        {
            CreateCard(files[i]);
        }
    }

    void CreateCard(string cardName)
    {
        cards.Add(new VisualElement { name = cardName });
        
        VisualElement card = cards.Q<VisualElement>(cardName); //Making card elem
        card.AddToClassList("Card");
        card.Add(new VisualElement { name = "Text-Container" });
        card.Add(new VisualElement { name = "Button-Container" });

        VisualElement textContainer = card.Q<VisualElement>("Text-Container"); //Making text contain
        textContainer.Add(new Label { text = cardName });

        VisualElement buttonContainer = card.Q<VisualElement>("Button-Container"); //Making button contain
        buttonContainer.Add(new Button { text = "Start", name = "Start" });
        buttonContainer.Add(new Button { text = "Edit", name = "Edit" });

        buttonContainer.Q<Button>("Start").clicked += () => { ButtonInListPressed(cardName, 1); };
        buttonContainer.Q<Button>("Edit").clicked += () => { ButtonInListPressed(cardName, 0); };
    }

    void ButtonInListPressed(string configName, int mode)
    {
        /*
         * Mode
         * 0 - Edit
         * 1 - Play
         * 2 - Delete
         */

        ConfigurationFunctions.ApplyConfig(ConfigurationFunctions.LoadFromFile(configName));

        switch (mode)
        {
            case 0:
                Debug.Log("Trying to edit " + configName);
                SceneManager.LoadScene("CustomSelectMenu");
                break;

            case 1:
                Debug.Log("Trying to play " + configName);
                SceneManager.LoadScene("ButterHunt");
                break;

            default:
                break;
        }
    }
}
