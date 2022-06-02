using UnityEngine.UIElements;
using UnityEngine;
using System;

public class PopupsUI : MonoBehaviour
{
    [SerializeField]
    VisualElement main;

    void RemovePopup()
    {
        main.Remove(main.Q<VisualElement>("pop-back"));
        main.Remove(main.Q<VisualElement>("pop-contain"));
    }

    public void SpawnpopEnterText (
        string title,
        string fillerText,
        string buttonGreenText,
        string buttonRedText,
        VisualElement elem,
        Action<string> greenButtonAction 
        )

    {
        main = elem;
        elem.Add(new VisualElement { name = "pop-back" });
        elem.Add(new VisualElement { name = "pop-contain" });

        elem = main.Q<VisualElement>("pop-contain");
        elem.Add(new VisualElement { name = "pop-body" });

        elem = main.Q<VisualElement>("pop-body");
        elem.Add(new Label { name = "pop-title", text = title });
        elem.Add(new TextField { name = "pop-input", value = fillerText, });
        elem.Add(new VisualElement { name = "pop-buttons" });

        elem = main.Q<VisualElement>("pop-buttons");
        elem.Add(new Button { name = "pop-button-yes", text = buttonGreenText });
        elem.Add(new Button { name = "pop-button-no", text = buttonRedText });

        main.Q<Button>("pop-button-yes").clicked += () => { string input = main.Q<TextField>("pop-input").text; RemovePopup(); greenButtonAction(input); };
        main.Q<Button>("pop-button-no").clicked += () => { RemovePopup(); };
    }

    public void SpawnpopInfoRed(
    string title,
    string infoText,
    string buttonRedText,
    VisualElement elem,
    Action redButtonAction
    )

    {
        main = elem;
        elem.Add(new VisualElement { name = "pop-back" });
        elem.Add(new VisualElement { name = "pop-contain" });

        elem = main.Q<VisualElement>("pop-contain");
        elem.Add(new VisualElement { name = "pop-body" });

        elem = main.Q<VisualElement>("pop-body");
        elem.Add(new Label { name = "pop-title", text = title });
        elem.Add(new TextField { name = "pop-input", value = infoText, isReadOnly = true });
        elem.Add(new VisualElement { name = "pop-buttons" });

        elem = main.Q<VisualElement>("pop-buttons");
        elem.Add(new Button { name = "pop-button-no", text = buttonRedText });

        main.Q<Button>("pop-button-no").clicked += () => {  RemovePopup(); redButtonAction(); };
    }

}
