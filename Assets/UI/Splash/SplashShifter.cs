using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SplashShifter : MonoBehaviour
{
    public void ShowSplash(float time, VisualTreeAsset splash)
    {
        GetComponent<UIDocument>().visualTreeAsset = splash;
        GetComponent<UIDocument>().enabled = true;

        if (time > 0)
        {
            //yield return new WaitForSeconds(time);
        }
    }

    public void HideSplash()
    {
        GetComponent<UIDocument>().enabled = false;
    }
}
