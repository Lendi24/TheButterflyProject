using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeScript : MonoBehaviour
{
    float screenWidth;
    float screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
