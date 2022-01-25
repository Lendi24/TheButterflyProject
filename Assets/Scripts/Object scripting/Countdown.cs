using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static bool allowInputs;

    public GameObject countdownBar;
    private static bool countDown = false;

    public float countDownTime;
    public float refillTime = 10;
    static float k;

    public static void SetK(float time)
    {
        //Debug.Log("Look! Time! " + 1 /time);
        k = 1 / time;
        countDown = true;
    }

    private void Update()
    {
        /*if (countdownBar.transform.localScale.x != Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x * 2)
            countdownBar.transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x * 2, countdownBar.transform.localScale.y, countdownBar.transform.localScale.z);*/
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, transform.position.y, transform.position.z);
        if (countDown)
        {            
            transform.localScale = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x * 2 * (k * TimmerManagment.GetTimeLeft()), transform.localScale.y, transform.localScale.z);
            if(countDownTime <= 0)
            {
                countDown = false;
            }
        }
        
    }
}