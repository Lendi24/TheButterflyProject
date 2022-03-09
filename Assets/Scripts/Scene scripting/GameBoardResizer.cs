using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoardResizer
{
    public static Vector3 GetGameBoardSize()
    {
        Vector3 mainCameraTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
        Vector3 mainCameraBottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        return new Vector3((mainCameraBottomRight.x - mainCameraTopLeft.x) / 10, 1, (mainCameraBottomRight.y - mainCameraTopLeft.y) / 10);
    }
}
