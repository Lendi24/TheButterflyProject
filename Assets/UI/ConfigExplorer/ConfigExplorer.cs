using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Mirror.Discovery
{
    public class NetworkingDisc : MonoBehaviour
    {
        public static void ServerFound()
        {

        }
    }
}

public class ConfigExplorer : MonoBehaviour
{
    VisualElement cards, selectedCard;

    void ChangeSelectedCard(Button newSelectedCard)
    {
        SetButtonMode(true);
        int borderWidth = 5;
        Color borderColour = Color.white;

        if (selectedCard == newSelectedCard)
        {
            ButtonInListPressed(selectedCard.name, 1);
        }

        if (selectedCard != null)
        {
            selectedCard.style.borderLeftWidth = 0;
        }

        selectedCard = newSelectedCard;
        selectedCard.style.borderLeftColor = borderColour;
        selectedCard.style.borderLeftWidth = borderWidth;
    }

    void SetButtonMode(bool enable)
    {
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("start").SetEnabled(enable);
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("export").SetEnabled(enable);
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("delete").SetEnabled(enable);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").visible = true;
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").visible = false;

        //GetButtons: Needs selected
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("start").clicked += () => { ButtonInListPressed(selectedCard.name, 1); };
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("export").clicked += () => { ButtonInListPressed(selectedCard.name, 2);  };
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("delete").clicked += () => {  };

        //GetButtons: Does not need selected
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("editor").clicked += () => { 
            if (selectedCard == null) { SceneManager.LoadScene("CustomSelectMenu"); }
            else { ButtonInListPressed(selectedCard.name, 0); }
        };
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("back").clicked += () => { SceneManager.LoadScene("MainMenu");  };
        //GetComponent<UIDocument>().rootVisualElement.Q<Button>("import").clicked += () => { NetVar.netModeServer = false; SceneManager.LoadScene("Networking"); };
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("reset").clicked += () => 
        {
            selectedCard = null;
            ConfigurationFunctions.InitFolder();
            GetConfigFiles(false);
        };

        cards = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Cards");
        //CreateCard("Råger");
        GetConfigFiles(false);
        //cards.RegisterCallback<PointerDownEvent, string>(ButtonInListEditPressed, "te");
    }

    void GetConfigFiles(bool hasFailed)
    {
        cards.Clear();
        string[] files = ConfigurationFunctions.GetConfigFiles();

        if (files.Length == 0)
        {
            if (hasFailed)
            {
                Debug.LogError("Error! No config files detected and Init of folder failed.");
                SceneManager.LoadScene("CustomSelectMenu");
            }

            else
            {
                ConfigurationFunctions.InitFolder(); //Function calling itself! 
                GetConfigFiles(true);  //If this happens more then once, infinite loop will happen
            }
        }

        else
        {
            for (int i = 0; i < files.Length; i++)
            {
                CreateCard(files[i]);
            }
        }
        SetButtonMode(false);
    }

    void CreateCard(string cardName)
    {
        cards.Add(new Button { name = cardName });

        Button card = cards.Q<Button>(cardName); //Making card elem
        card.AddToClassList("Card");
        card.Add(new VisualElement { name = "Text-Container" });
        //card.Add(new VisualElement { name = "Button-Container" });
        card.clicked += () => { ChangeSelectedCard(card); };

        VisualElement textContainer = card.Q<VisualElement>("Text-Container"); //Making text contain
        textContainer.Add(new Label { text = cardName });

        /*
        VisualElement buttonContainer = card.Q<VisualElement>("Button-Container"); //Making button contain
        buttonContainer.Add(new Button { text = "Start", name = "Start" });
        buttonContainer.Add(new Button { text = "Edit", name = "Edit" });

        buttonContainer.Q<Button>("Start").clicked += () => { ButtonInListPressed(cardName, 1); };
        buttonContainer.Q<Button>("Edit").clicked += () => { ButtonInListPressed(cardName, 0); };*/

        
    }

    void ButtonInListPressed(string configName, int mode)
    {
        /*
         * Mode
         * 0 - Edit
         * 1 - Play
         * 2 - Share
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

            case 2:
                Debug.Log("Trying to share " + configName);
                NetVar.netModeServer = true; 
                SceneManager.LoadScene("Networking"); 
                break;

            default:
                break;
        }
    }
}
