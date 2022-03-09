using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitcherUSS : MonoBehaviour
{
    public StyleSheet ussSmallScreens;
    public StyleSheet ussBigScreens;

    float expectedDpi;
    string uiMode;

    int targetResolutionWith, targetResolutionHeight;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        uiMode = "none";
        targetResolutionWith = Screen.width;
        targetResolutionHeight = Screen.height;
        expectedDpi = GetComponent<UIDocument>().panelSettings.referenceDpi;
        root = GetComponent<UIDocument>().rootVisualElement;
        changeUIMode();
    }

    private void Update()
    {
        if (targetResolutionHeight != Screen.height || targetResolutionWith != Screen.width)
        {
            targetResolutionWith = Screen.width;
            targetResolutionHeight = Screen.height;
            changeUIMode();
        }
    }

    void changeUIMode()
    {
        if (uiMode != "smallScreen")
        {
            if (Screen.height / (Screen.dpi / expectedDpi) < 420 || Screen.width / (Screen.dpi / expectedDpi) < 470)
            {
                Debug.Log("Changing to smallScreensUSS...");
                uiMode = "smallScreen";
                root.styleSheets.Add(ussSmallScreens);
                try { root.styleSheets.Remove(ussBigScreens); } catch { }
            }
        }

        if (uiMode != "bigScreen")
        {
            if (Screen.height / (Screen.dpi / expectedDpi) > 420 && Screen.width / (Screen.dpi / expectedDpi) > 470)
            {
                Debug.Log("Changing to bigScreensUSS...");
                uiMode = "bigScreen";
                root.styleSheets.Add(ussBigScreens);
                try { root.styleSheets.Remove(ussSmallScreens); } catch { Debug.LogError(""); }
            }
        }
    }
}
