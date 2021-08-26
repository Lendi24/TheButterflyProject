using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject butterfly;

    [SerializeField]
    Material backgroundPattern;

    [SerializeField]
    float preHuntTime, huntTime;

    [SerializeField]
    int butterflyAmount, gameState;

    [SerializeField]
    bool timmerRunning;

    GameObject[] butterflies;

    /*
    0-PreGame
    1-PreHunt
    2-Hunt
    3-PostHunt
    4-PostGame
     */

    // Start is called before the first frame update
    void Start()
    {
        //Init variables
        gameState = 0;
        butterflyAmount = 5;
        butterflies = new GameObject[butterflyAmount];

        //[INSER MENU HERE]

        GameStart();
    }

    void GameStart()
    {
        for (int i = 0; i < butterflyAmount; i++)
        {
            Vector2 boardSize = GetComponent<Renderer>().bounds.size;

            GameObject newButterfly = Instantiate(butterfly,
                new Vector3(
                    Random.Range((boardSize.x / 2) - butterfly.GetComponent<Renderer>().bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2),
                    Random.Range((boardSize.y / 2) - butterfly.GetComponent<Renderer>().bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2),
                    (butterfly.GetComponent<Renderer>().bounds.size.z)/-2),
                Quaternion.Euler(0,0,Random.Range(1,360)));

            newButterfly.transform.parent = this.transform;
            newButterfly.transform.name =  "Butterfly id:"+i;
            butterflies[i] = newButterfly;
        }

        gameState = 1;
        //TODO: Add splash and countdown
        gameState = 2;
    }

    /*
    |=============================|
    |==BUTTERFLY EVENT HANDELERS==|
    |=============================|
    */
    public void RandomMoveButterfly(GameObject butterfly)
    {
        Vector2 boardSize = GetComponent<Renderer>().bounds.size;

        butterfly.transform.position = new Vector3(
                    Random.Range((boardSize.x / 2) - butterfly.GetComponent<Renderer>().bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2),
                    Random.Range((boardSize.y / 2) - butterfly.GetComponent<Renderer>().bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2),
                    (butterfly.GetComponent<Renderer>().bounds.size.z) / -2);

        butterfly.transform.rotation = Quaternion.Euler(0, 0, Random.Range(1, 360));

        Debug.Log("Butterfly overlap detected: Moved "+butterfly.name+" to a new random position!");
    }

    public void ButterClick(GameObject butterfly)
    {
        if (gameState == 2)
        {
            Debug.Log("Butterfly click detected: Removed " + butterfly.name + " from the game board");
            Destroy(butterfly);
            gameState = 3;
        }
    }
}
