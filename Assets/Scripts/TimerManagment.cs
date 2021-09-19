using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimmerManagment
{
    private static float timmerTimeLeft;
    private static bool timmerIsRunning;

    public static bool Timmer(float time)
    {
        if (!timmerIsRunning)
        {
            timmerTimeLeft = time;
            timmerIsRunning = true;
        }

        if (timmerTimeLeft > 0)
        {
            timmerTimeLeft -= Time.deltaTime;
            return false;
        }

        else
        {
            timmerIsRunning = false;
            return true;
        }

    }
}
