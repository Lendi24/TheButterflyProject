using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject butterfly, preGameSplash, postGameSplash, butterContainer;

    [SerializeField]
    Material backgroundPattern, animalMaterial;

    [SerializeField]
    Texture backgroundTexture, blendTexture;

    [SerializeField]
    float preHuntTime, huntTime, tilesPerUnit;

    [SerializeField]
    int butterflyGeneLength, butterflyStartAmountRandom, butteflyStartAmountGene, maximumKills, minimumKills, butterflyRenderMode, butterflyRoundSpawnAmount;

    [SerializeField]
    bool resetEverythingOnNextGen;

    private int butterfliesRemaining, gameState;
    string keyPrefix = "modelMatch";

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
        if (blendTexture == null)
        {
            Debug.LogWarning("No blend-texture selected!\nDefaulting to colour blending...");
        }

        if (backgroundTexture == null)
        {
            Debug.LogWarning("No background-texture selected!\nDefaulting to perlin noise...");
        }

        GetComponent<Renderer>().material = backgroundPattern;
        GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit,
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));

        Physics.autoSyncTransforms = true;
        ResetVariables();
        PrepareGame();
    }

    void ResetVariables()
    {
        //Init variables
        gameState = 0;
        butterfliesRemaining = butterflyStartAmountRandom;
        preGameSplash.GetComponent<Canvas>().enabled = true;
        postGameSplash.GetComponent<Canvas>().enabled = false;
    }

    void PrepareGame()
    {
        if (butteflyStartAmountGene > 0)
        {
            for (int i = 0; i < butteflyStartAmountGene; i++)
            {
                for (int j = 0; j <= butterflyGeneLength; j++)
                {
                    SpawnButterfly(GeneticManager.GiveSpecificGenetics(butterflyGeneLength, j));
                }
            }
        }

        for (int i = 0; i < butterflyStartAmountRandom - ((1+butterflyGeneLength) * butteflyStartAmountGene); i++)
        {
            SpawnButterfly(GeneticManager.GiveRandomGenetics(butterflyGeneLength));
        }

        gameState = 1;
    }

    Vector3 RandomButterPos(Quaternion newButterRotate)
    {
        Vector2 boardSize = GetComponent<Renderer>().bounds.size;
        float newButterX;
        float newButterY;
        float newButterZ = ((butterfly.GetComponent<Renderer>().bounds.size.z) / -2);
        int nrOfLoops = 0;
        bool noOverlap;

        do
        { //Finds empty space to spawn butterfly
            nrOfLoops++;
            newButterX = Random.Range((boardSize.x / 2) - butterfly.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2, (boardSize.x / -2) + butterfly.GetComponent<Renderer>().bounds.size.x / 2);
            newButterY = Random.Range((boardSize.y / 2) - butterfly.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2, (boardSize.y / -2) + butterfly.GetComponent<Renderer>().bounds.size.y / 2);

            noOverlap = !Physics.CheckBox(new Vector3(newButterX, newButterY, newButterZ), 
                     butterfly.GetComponent<Renderer>().bounds.size / 2, newButterRotate);

        } while (!(nrOfLoops > 500000 || noOverlap)); //Break for infinite loop 
        if (!noOverlap) Debug.LogError("Could not find space for butterfly, or code is broken.");
        
        return new Vector3(newButterX, newButterY, newButterZ);
    }

    void SpawnButterfly(bool[] genetics)
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(1, 360));
        GameObject newButterfly = Instantiate(butterfly,                //Prefab
                                      RandomButterPos(randomRotation),      //Random pos, without overlapp 
                                      randomRotation);                      //Random rot. Needs to be pre-calculated for col detect

        newButterfly.transform.name = "Butterfly";
        newButterfly.transform.parent = butterContainer.transform;
        newButterfly.GetComponent<ButterflyBehaviour>().gameBoard = this.gameObject;

        newButterfly.GetComponent<ButterflyBehaviour>().genes = genetics;
        float blendIn = GeneticManager.BlendInCalc(genetics);

        switch (butterflyRenderMode)
        {
            case 0://alpha mode !THIS IS OLD! Use camo shader for this effect with transparant material if possible!
                Color newColor = Color.white;
                Material newMat = new Material(Shader.Find("Transparent/Diffuse"));
                newColor.a = blendIn;
                newButterfly.GetComponent<Renderer>().material = newMat;
                newButterfly.GetComponent<Renderer>().material.color = newColor;
                GetComponent<Renderer>().material.SetFloat("_enablePerlin", 0);
                break;

            case 1://texture-matched mode
                TextureMatchedRender();
                newButterfly.GetComponent<Renderer>().material.SetFloat("_LerpValue", blendIn);
                newButterfly.GetComponent<Renderer>().material.SetFloat("_enablePerlin", 0);
                GetComponent<Renderer>().material.SetFloat("_enablePerlin", 0);
                break;

            case 2://texture-matched perlin mode
                TextureMatchedRender();
                newButterfly.GetComponent<Renderer>().material.SetFloat("_LerpValue", blendIn);
                newButterfly.GetComponent<Renderer>().material.SetFloat("_enablePerlin", blendIn);
                GetComponent<Renderer>().material.SetFloat("_enablePerlin", 1);
                break;

            default:
                break;
        }

        void TextureMatchedRender() {

            try
            {
                string modelName = newButterfly.GetComponent<MeshFilter>().sharedMesh.name;
                if (!PlayerPrefs.HasKey(GetVariable.GetKeyPrefix() + modelName)) TextureMatchManager.reset();

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

                newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", blendTexture);
                newButterfly.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
                    newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * newButterfly.transform.localScale.x * butterMatchX * squareY,
                    newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * newButterfly.transform.localScale.y * butterMatchY * squareX));

                newButterfly.GetComponent<Renderer>().material.SetTexture("_SecondaryTex", backgroundTexture);
                newButterfly.GetComponent<Renderer>().material.SetTextureScale("_SecondaryTex", new Vector2(
                    newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * newButterfly.transform.localScale.x * butterMatchX * squareY,
                    newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * newButterfly.transform.localScale.y * butterMatchY * squareX));
            }

            catch (System.Exception)
            {
                Debug.LogError("Could not load correct material for model. Try rendermode 0, or give texture a size to scale");
            }


        }
    }
    

    private void Update()
    {
        switch (gameState)
        {
            case 1:

                if (TimmerManagment.Timmer(preHuntTime))
                {
                    preGameSplash.GetComponent<Canvas>().enabled = false;
                    gameState = 2;
                }

                break;

            case 2:

                if (TimmerManagment.Timmer(huntTime))
                {
                    postGameSplash.GetComponent<Canvas>().enabled = true;

                    if ((butterflyStartAmountRandom - butterfliesRemaining) < minimumKills)
                    {
                        Debug.Log("U loose");
                        gameState = 4;//Failed! Health will be lost, energy will be lost or game will be lost here.
                    }

                    else
                    {
                        Debug.Log("continu");

                        foreach (Transform animal in butterContainer.transform)
                        {
                            animal.transform.position = new Vector3(0, 0, 5);
                        }


                        for (int i = 0; i < butterContainer.transform.childCount; i++)
                        {
                            Quaternion newRot = Quaternion.Euler(0, 0, Random.Range(1, 360));
                            Vector3 newPos = RandomButterPos(newRot);

                            butterContainer.transform.GetChild(i).transform.position = newPos;
                            butterContainer.transform.GetChild(i).rotation = newRot;
                        }


                        gameState = 3;//Continue playing!
                    }
                }

                break;

            case 3:

                for (int i = 0; i < butterflyRoundSpawnAmount; i++) SpawnButterfly(GeneticManager.EvolveNewAnimal(butterContainer.GetComponent<ButterCollection>().GetAnimalGenes()));
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

    void GetButterflies()
    {
        
    }

    /*
    |=============================|
    |==BUTTERFLY EVENT HANDELERS==|
    |=============================|
    */

    public void ButterClick(GameObject butterfly)
    {
        if (gameState == 2 && (butterflyStartAmountRandom - butterfliesRemaining) < maximumKills)
        {
            Destroy(butterfly);
            Debug.Log("Butterfly click detected: Removed " + butterfly.name + " from the game board");
            butterfliesRemaining--;
        }
    }
}
