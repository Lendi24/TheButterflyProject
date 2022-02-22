using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToWindow : MonoBehaviour
{
    public float screenWidthLimi = 600;
    private float camSize;
    private int targetScreenSizeX, targetScreenSizeY;

    private float size;
    private float ratio;
    private float screenHeight;

    [SerializeField]
    GameObject gm;

    void Awake()
    {
        CameraResize();
    }

    void Start()
    {
        targetScreenSizeX = Screen.width;
        targetScreenSizeY = Screen.height;
        camSize = float.Parse(GetComponent<Camera>().orthographicSize.ToString()); //workaround to copy value
    }

    void Update()
    {
        CheckForResize();
    }

    void CameraResize()
    {

        if (targetScreenSizeY > targetScreenSizeX)
        {
            ratio = (float)Screen.height / (float)Screen.width;
            size = camSize * ratio;
            GetComponent<Camera>().orthographicSize = size;
        }

        else
        {
            GetComponent<Camera>().orthographicSize = 5;
        }
    }

    void CheckForResize()
    {
        if (targetScreenSizeX != Screen.width || targetScreenSizeY != Screen.height)
        {
            targetScreenSizeX = Screen.width;
            targetScreenSizeY = Screen.height;

            //Stuff to do when screen resize 
            CameraResize();
            gm.GetComponent<GameManager>().SetScreenSize();
        }
    }
}
