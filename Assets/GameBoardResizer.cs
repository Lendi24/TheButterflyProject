using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardResizer
{

    public static Vector3 GetGameBoardSize()
    {
        Vector3 CamFirstPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 CamLastPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector3((CamLastPos.x - CamFirstPos.x) / 10, 1, (CamLastPos.y - CamFirstPos.y) / 10);
    }

    /*public static Vector3 GetGameBoardSize()
    {
        Vector3 CamFirstPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 CamLastPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector3((CamLastPos.x - CamFirstPos.x) / 10, 1, (CamLastPos.y - CamFirstPos.y) / 10);
    }*/

}
