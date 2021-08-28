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
    Texture backgroundTexture, blendTexture;

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

        //[INSER MENU HERE]
        butterflies = new GameObject[butterflyAmount];


        GameStart();
    }

    void GameStart()
    {
        for (int i = 0; i < butterflyAmount; i++)
        {
            Vector2 boardSize = GetComponent<Renderer>().bounds.size;
            float newButterX;
            float newButterY;
            float newButterZ = ((butterfly.GetComponent<Renderer>().bounds.size.z) / -2);
            Quaternion newButterRotate;
            int nrOfLoops = 0;
            bool noOverlap;

            do { //Finds empty space to spawn butterfly
                nrOfLoops++;
                newButterX = Random.Range((boardSize.x / 2) - butterfly.GetComponent<Renderer>().bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2);
                newButterY = Random.Range((boardSize.y / 2) - butterfly.GetComponent<Renderer>().bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2);
                newButterRotate = Quaternion.Euler(0, 0, Random.Range(1, 360));

                noOverlap = !Physics.CheckBox(new Vector3(newButterX, newButterY, newButterZ),
                                       butterfly.GetComponent<Renderer>().bounds.size / 2, newButterRotate);

            } while (!(nrOfLoops > 5000000 || noOverlap));

            if (nrOfLoops > 5000000)
            {
                Debug.LogError("Could not find space for butterfly, or spawner code is broken.");
            }

            GameObject newButterfly = Instantiate(butterfly,
                new Vector3(
                    newButterX, newButterY, newButterZ), newButterRotate);

            newButterfly.transform.parent = this.transform;
            newButterfly.transform.name =  "Butterfly id:"+i;
            newButterfly.GetComponent<Renderer>().material = backgroundPattern;
            newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
            newButterfly.GetComponent<Renderer>().material.SetTexture("__SecondaryTex", blendTexture);

            butterflies[i] = newButterfly;
        }
        /*
        for (int i = 0; i < butterflyAmount; i++)
        {
            butterflies[i].GetComponent<Rigidbody>().isKinematic = true;
            butterflies[i].GetComponent<BoxCollider>().enabled = false;
            butterflies[i].GetComponent<MeshCollider>().enabled = true;
        }
        */
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
