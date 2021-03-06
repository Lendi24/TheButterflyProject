using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ConfigCard
{
    public bool isLocal;
    public string name;
    public string origin;
    public string version;
    public Button visualObj;
    public System.Uri uri;
}

public static class NetMode
{
    public static int mode;
}

public class ConfigExplorer : MonoBehaviour
{
    VisualElement localCards, remoteCards;
    ConfigCard selectedCard;

    [SerializeField]
    NetworkingSelect netscript;

    void ChangeSelectedCard(ConfigCard newSelectedCard)
    {
        SetButtonMode(true);
        int borderWidth = 5;
        Color borderColour = Color.white;
        
        if (selectedCard != null)
        {
            /*
            if (selectedCard.origin == newSelectedCard.origin)
            {
                ButtonInListPressed(selectedCard.origin, 1);
            }

            else
            {*/
            selectedCard.visualObj.style.borderLeftWidth = 0;


            //}
        }
        selectedCard = newSelectedCard;
        selectedCard.visualObj.style.borderLeftColor = borderColour;
        selectedCard.visualObj.style.borderLeftWidth = borderWidth;

        if (selectedCard.isLocal)
        {
            GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").style.display = DisplayStyle.Flex;
            GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").style.display = DisplayStyle.None;
        }

        else
        {
            GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").style.display = DisplayStyle.None;
            GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").style.display = DisplayStyle.Flex;
        }
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

        //GetButtons: Needs selected, remote
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").Q<Button>("start").clicked += () => { ButtonInListPressed(selectedCard.uri, 3); };
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").Q<Button>("import").clicked += () => { ButtonInListPressed(selectedCard.uri, 4); };

        //GetButtons: Needs selected, local
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("start").clicked += () => { ButtonInListPressed(selectedCard.origin, 1); };
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("export").clicked += () => { ButtonInListPressed(selectedCard.origin, 2);  };
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").Q<Button>("delete").clicked += () => {
            ConfigurationFunctions.RemoveFile(selectedCard.origin);
            selectedCard = null;
            GetConfigFiles(false);
        };

        //GetButtons: Does not need selected
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("editor").clicked += () => { 
            if (selectedCard == null) { SceneManager.LoadScene("CustomSelectMenu"); }
            else { if (selectedCard.uri == null) { ButtonInListPressed(selectedCard.origin, 0);
                } else { ButtonInListPressed(selectedCard.uri, 0); }}
        };
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("back").clicked += () => { SceneManager.LoadScene("MainMenu");  };
        //GetComponent<UIDocument>().rootVisualElement.Q<Button>("import").clicked += () => { NetVar.netModeServer = false; SceneManager.LoadScene("Networking"); };
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("reset").clicked += () => 
        {
            selectedCard = null;
            ConfigurationFunctions.InitFolder();
            GetConfigFiles(false);
        };

        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("local-row1").style.display = DisplayStyle.Flex;
        GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("remote-row1").style.display = DisplayStyle.None;

        localCards = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("LocalCards");
        remoteCards = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RemoteCards");

        //CreateCard("R?ger");
        GetConfigFiles(false);
        //cards.RegisterCallback<PointerDownEvent, string>(ButtonInListEditPressed, "te");
    }

    void GetConfigFiles(bool hasFailed)
    {
        localCards.Clear();
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
                CreateCard(new ConfigCard { 
                    name = ConfigurationFunctions.LoadFromFile(files[i]).confName,
                    origin = files[i],
                    isLocal = true},
                    localCards);
            }
        }
        SetButtonMode(false);
    }

    public void GetRemoteCards(ConfigCard[] newRemoteCards)
    {
        remoteCards = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RemoteCards");
        remoteCards.Clear();

        for (int i = 0; i < newRemoteCards.Length; i++)
        {
            CreateCard(newRemoteCards[i], remoteCards);
        }
    }
    /*
    public void RemoveCard(ConfigCard oldCard, VisualElement board) Not used! Should be, tho. FIX!
    {
        board.Remove(oldCard.visualObj);
    }*/

    public void CreateCard(ConfigCard newCard, VisualElement board)
    {
        board.Add(new Button { name = newCard.origin });
        newCard.visualObj = board.Q<Button>(newCard.origin); //Making card elem
        newCard.visualObj.AddToClassList("Card");
        newCard.visualObj.Add(new VisualElement { name = "Text-Container" });
        //card.Add(new VisualElement { name = "Button-Container" });
        newCard.visualObj.clicked += () => {
            ChangeSelectedCard(newCard); ;};

        VisualElement textContainer = newCard.visualObj.Q<VisualElement>("Text-Container"); //Making text contain
        textContainer.Add(new Label { name = "TitleText", text = newCard.name });
        textContainer.Add(new Label { name = "SourceText", text = newCard.origin });

        /*
        VisualElement buttonContainer = card.Q<VisualElement>("Button-Container"); //Making button contain
        buttonContainer.Add(new Button { text = "Start", name = "Start" });
        buttonContainer.Add(new Button { text = "Edit", name = "Edit" });

        buttonContainer.Q<Button>("Start").clicked += () => { ButtonInListPressed(cardName, 1); };
        buttonContainer.Q<Button>("Edit").clicked += () => { ButtonInListPressed(cardName, 0); };*/


    }

    void ButtonInListPressed(string configName, int mode) //Local
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
                netscript.ShareConfig();

                GetComponent<PopupsUI>().SpawnpopInfoRed(
                title: "Sharing config over network...",
                infoText: "Configuration file is now visible to other users on this network!",
                buttonRedText: "Stop",
                elem: GetComponent<UIDocument>().rootVisualElement,
                redButtonAction: netscript.StopSharingConfig
                );


                break;

            default:
                break;
        }
    }

    void ButtonInListPressed(System.Uri configName, int mode) //Remote
    {
        /*
         * Mode
         * 0 - Edit
         * 3 - Play
         * 4 - Download
         */

        NetMode.mode = mode;
        CurrentConfig.conf = null;
        netscript.Connect(new Mirror.Discovery.ServerResponse { uri = configName });
    }

    public void PostConnectCallback(int mode)
    {
        switch (mode)
        {
            case 0:
                SceneManager.LoadScene("CustomSelectMenu");
                break;

            case 3:
                SceneManager.LoadScene("ButterHunt");
                break;

            case 4:
                GetComponent<PopupsUI>().SpawnpopEnterText(
                    title: "Enter new config-file name",
                    fillerText: "Name here",
                    buttonGreenText: "Confirm",
                    buttonRedText: "Cancel",
                    elem: GetComponent<UIDocument>().rootVisualElement,
                    greenButtonAction: PopupGiveNameCallback
                );
                break;

            default:
                break;
        }
    }

    void PopupGiveNameCallback (string name)
    {
        ConfigurationFunctions.SaveToFile(CurrentConfig.conf, name);
        SceneManager.LoadScene("ConfigExplorer");
    }



    /*            case 3:
                Debug.Log("Trying to play remote config " + configName);
                GetComponent<NetworkingSelect>().Connect(new Mirror.Discovery.ServerResponse { uri = new System.Uri { AbsolutePath = configName });
*/
}
