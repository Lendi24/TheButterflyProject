using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool isRunning = false;

    private void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                TimeFormat(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                isRunning = false;
            }
        }
    }

    string TimeFormat(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
