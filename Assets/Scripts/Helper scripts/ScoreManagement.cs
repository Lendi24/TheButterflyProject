using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManagement
{
    private static float remainingTime;
    private static int points;
    public static int CalculateScore(float _remainingTime)
    {
        remainingTime = _remainingTime;
        points = Mathf.RoundToInt(10f * remainingTime);
        return points;
    }
}
