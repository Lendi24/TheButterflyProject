using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject butterfly, preGameSplash, postGameSplash;

    [SerializeField]
    Material backgroundPattern, animalMaterial;

    [SerializeField]
    Texture backgroundTexture, blendTexture;

    [SerializeField]
    float preHuntTime, huntTime, tilesPerUnit;

    [SerializeField]
    int butterflyAmount, maximumKills, minimumKills, butterflyRenderMode;

    private int butterfliesRemaining, gameState;
    string keyPrefix = "modelMatch";

    GameObject[] butterflies;

    /* Butterfly Render Modes
     * 0 - Transparancy
     * 1 - TextureMode
    */

    /* Game States
    0-PreGame
    1-PreHunt
    2-Hunt
    3-PostHunt
    4-PostGame
     */

    // Start is called before the first frame update
    void Start()
    {
        //[INSER MENU HERE]

        GetComponent<Renderer>().material = backgroundPattern;
        GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit, 
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));

        ResetVariables();
        PrepareGame();
    }

    void ResetVariables()
    {
        //Init variables
        gameState = 0;
        butterfliesRemaining = butterflyAmount;
        butterflies = new GameObject[butterflyAmount];
        preGameSplash.GetComponent<Canvas>().enabled = true;
        postGameSplash.GetComponent<Canvas>().enabled = false;
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

            } while (!(nrOfLoops > 500000 || noOverlap)); //Break for infinite loop 

            if (nrOfLoops > 500000)//Throws error for infinite loop
            {
                Debug.LogError("Could not find space for butterfly, or spawner code is broken.");
            }

            GameObject newButterfly = Instantiate(butterfly,//Spawn butterfly
                new Vector3(
                    newButterX, newButterY, newButterZ), newButterRotate);

            newButterfly.transform.name =  "Butterfly id:"+i;

            butterflies[i] = newButterfly;

            switch (butterflyRenderMode)
            {
                case 0:
                    Color newColor = Color.white;
                    Material newMat = new Material(Shader.Find("Transparent/Diffuse"));
                    float value = Random.Range(0, 11);
                    value /= 10;
                    newColor.a = value;
                    newButterfly.GetComponent<Renderer>().material = newMat;
                    newButterfly.GetComponent<Renderer>().material.color = newColor;
                    newButterfly.GetComponent<ButterflyBehaviour>().gameBoard = this.gameObject;
                    break;

                case 1:
                    
                    string modelName = newButterfly.GetComponent<MeshFilter>().sharedMesh.name;
                    Debug.Log(modelName);
                    string[] tempData = PlayerPrefs.GetString(keyPrefix + modelName).Split(':');

                    float butterMatchX = float.Parse(tempData[0]);
                    float butterMatchY = float.Parse(tempData[1]);
                    bool squareMatch = bool.Parse(tempData[2]);

                    float squareX, squareY;

                    if (squareMatch)
                    {
                        squareX = newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x;
                        squareY = newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y;
                    }

                    else
                    {
                        squareX = 1;
                        squareY = 1;
                    }

                    newButterfly.GetComponent<Renderer>().material = animalMaterial;
                    newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
                    newButterfly.GetComponent<Renderer>().material.SetTexture("__SecondaryTex", blendTexture);
                    newButterfly.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
                        newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * newButterfly.transform.localScale.x * butterMatchX * squareY,
                        newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * newButterfly.transform.localScale.y * butterMatchY * squareX));

                    GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
                        GetComponent<Renderer>().bounds.size.x * tilesPerUnit,
                        GetComponent<Renderer>().bounds.size.y * tilesPerUnit));


                    break;
                    
                default:
                    break;
            }
        }

        gameState = 1;
    }

    private void Update()
    {
        switch (gameState)
        {
            case 1:

                if (TimmerManagment.Timmer(preHuntTime)) {
                    preGameSplash.GetComponent<Canvas>().enabled = false;
                    gameState = 2;
                }

                break;

            case 2:

                if (TimmerManagment.Timmer(huntTime)) {
                    postGameSplash.GetComponent<Canvas>().enabled = true;

                    if ((butterflyAmount - butterfliesRemaining) < minimumKills)
                    {
                        Debug.Log("U loose");
                        gameState = 4;//Failed! Health will be lost, energy will be lost or game will be lost here.
                    }

                    else
                    {
                        Debug.Log("continu");
                        gameState = 3;//Continue playing!
                    }
                }

                break;

            case 3:

                ResetVariables();
                gameState = 1;
                //Fix some splash about stats or smt and some wait time

                break;

            case 4:

                //Fix some splash about stats or smt and some wait time

                break;


            default:
                break;
        }
    }

    /*
    |=============================|
    |==BUTTERFLY EVENT HANDELERS==|
    |=============================|
    */

    public void ButterClick(GameObject butterfly)
    {
        if (gameState == 2 && (butterflyAmount - butterfliesRemaining) < maximumKills)
        {
            Destroy(butterfly);
            Debug.Log("Butterfly click detected: Removed " + butterfly.name + " from the game board");
            butterfliesRemaining--;
        }
    }
}
