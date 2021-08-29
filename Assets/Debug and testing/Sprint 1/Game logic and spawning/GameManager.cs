using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject butterfly, preGameSplash, postGameSplash;

    [SerializeField]
    Material backgroundPattern;

    [SerializeField]
    Texture backgroundTexture, blendTexture;

    [SerializeField]
    float preHuntTime, huntTime, tilesPerUnit;

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

        GetComponent<Renderer>().material = backgroundPattern;
        GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit, 
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));

        PrepareGame();
    }

    void PrepareGame()
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
                newButterX = Random.Range((boardSize.x / 2) - butterfly.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2);
                newButterY = Random.Range((boardSize.y / 2) - butterfly.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2);
                newButterRotate = Quaternion.Euler(0, 0, Random.Range(1, 360));

                noOverlap = !Physics.CheckBox(new Vector3(newButterX, newButterY, newButterZ),
                                       butterfly.GetComponent<Renderer>().bounds.size / 2, newButterRotate);

            } while (!(nrOfLoops > 500000 || noOverlap));

            if (nrOfLoops > 500000)
            {
                Debug.LogError("Could not find space for butterfly, or spawner code is broken.");
            }

            GameObject newButterfly = Instantiate(butterfly,
                new Vector3(
                    newButterX, newButterY, newButterZ), newButterRotate);

            //newButterfly.transform.parent = this.transform; DO NOT ENABLE!! Childning game object will cause weird scaling behaviour 
            newButterfly.transform.name =  "Butterfly id:"+i;
            newButterfly.GetComponent<Renderer>().material = backgroundPattern;
            newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
            newButterfly.GetComponent<Renderer>().material.SetTexture("__SecondaryTex", blendTexture);
            newButterfly.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
                newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * newButterfly.transform.localScale.x * 2, //I am not quite sure why *2 fixes it,
                newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * newButterfly.transform.localScale.y * 2));//but I think it has to do with how x and y is messured by different functions
                                                                                                                                      //A.K.A Don't touch it, it works :3

            float value = Random.Range(0, 11);
            value /= 10;
            newButterfly.GetComponent<Renderer>().material.SetFloat("_LerpValue", value);

            butterflies[i] = newButterfly;
        }
        /* Code from when I tried to fix collition detection the easy way, that later turned out to be the hard way
        for (int i = 0; i < butterflyAmount; i++)
        {
            butterflies[i].GetComponent<Rigidbody>().isKinematic = true;
            butterflies[i].GetComponent<BoxCollider>().enabled = false;
            butterflies[i].GetComponent<MeshCollider>().enabled = true;
        } 
        */


        gameState = 1;
        //TODO: Add splash and countdown
//        CountdownSplash();//working on it
 //       gameState = 2;
    }

    private void Update()
    {
        switch (gameState)
        {
            case 1:
                preGameSplash.GetComponent<Canvas>().enabled = true;

                if (preHuntTime > 0)
                {
                    preHuntTime -= Time.deltaTime;
                }

                else
                {
                    preGameSplash.GetComponent<Canvas>().enabled = false;
                    gameState = 2;
                }

                break;

            case 2:
                postGameSplash.GetComponent<Canvas>().enabled = false;

                if (huntTime > 0)
                {
                    huntTime -= Time.deltaTime;
                }

                else
                {
                    postGameSplash.GetComponent<Canvas>().enabled = true;
                    gameState = 3;
                }


                break;

            case 3:
                break;

            default:
                break;
        }
    }

    /*
    |=============================|
    |==BUTTERFLY EVENT HANDELERS==|
    |=============================|
    
    public void RandomMoveButterfly(GameObject butterfly)
    {
        Vector2 boardSize = GetComponent<Renderer>().bounds.size; This code is old. We dont use this code. We do not speak of this code. 

        butterfly.transform.position = new Vector3(
                    Random.Range((boardSize.x / 2) - butterfly.GetComponent<Renderer>().bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2),
                    Random.Range((boardSize.y / 2) - butterfly.GetComponent<Renderer>().bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2),
                    (butterfly.GetComponent<Renderer>().bounds.size.z) / -2);

        butterfly.transform.rotation = Quaternion.Euler(0, 0, Random.Range(1, 360));

        Debug.Log("Butterfly overlap detected: Moved "+butterfly.name+" to a new random position!");
    }*/

    public void ButterClick(GameObject butterfly)
    {
        if (gameState == 2)
        {
            Debug.Log("Butterfly click detected: Removed " + butterfly.name + " from the game board");
            gameState = 3;
        }
    }
}
