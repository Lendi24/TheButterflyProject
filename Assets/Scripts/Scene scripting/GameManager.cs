using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject butterfly, butterContainer, deadButterContainer, spriteOverlayMan;

    [SerializeField]
    VisualTreeAsset preHuntSplash, gameOverSplash, pauseMenu;

    [SerializeField]
    Material backgroundPattern, animalMaterial;

    [SerializeField]
    Texture backgroundTexture, blendTexture;

    [SerializeField]
    Mesh destroyedButterfly;

    [SerializeField]
    float preHuntTime, huntTime, tilesPerUnit, huntTimeReducePercent, huntTimeMin, renderLerp, renderPerlin;

    [SerializeField]
    int butterflyGeneLength, ButterlfyTotalStartAmnt, maximumKills, minimumKills, butterflyRoundSpawnAmount, healthAmount, score;
    int liveMaximumKills, geneMode, initAmntOfWhite, initAmountOfMixed, initAmountOfDark;

    [SerializeField]
    bool resetEverythingOnNextGen, noSafeClick, keepButterAmount, renderButterBackground;

    private bool isPaused;
    private int gameState;
    readonly string keyPrefix = "modelMatch";
    List<GameObject> deadButterflies;
    List<StatSave> statsLogList = new List<StatSave>();
    static int rounds;

    /* Game States
    0-PreGame
    1-PreHunt
    2-Hunt
    3-PostHunt
    4-PostGame
     */

    // Start is called before the first frame update
    public void Start()
    {
        butterflyGeneLength = 2;
        Time.timeScale = 1;
        GetComponent<SplashShifter>().ShowSplash(0, preHuntSplash);
        Physics.autoSyncTransforms = true;
        GetVariables();
        ClearBoard();
        spriteOverlayMan.GetComponent<SpriteOverlay>().MakeHealthSpriteUI(healthAmount);
        SetScreenSize();
        PrepareGame();
        ResetVariables();
    }

    public void SetScreenSize()
    {
        transform.localScale = GameBoardResizer.GetGameBoardSize();

        //Making size correct
        GetComponent<Renderer>().material = backgroundPattern;
        GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit,
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));

        //Shader render mode
        GetComponent<Renderer>().material.SetFloat("_LerpValue", renderLerp);
        GetComponent<Renderer>().material.SetFloat("_enablePerlin", renderPerlin);

        GetComponent<Renderer>().material.SetTexture("_MainTex", null);
        GetComponent<Renderer>().material.SetTexture("_SecondaryTex", backgroundTexture);


        // 1,0: Textured
        // 1,1: Mixed
        // 0,1: Perlin

        /*foreach(Transform selectedButterfly in butterContainer.GetComponentInChildren<Transform>())
        {
            Debug.Log(butte    void TextureMatchedRender(GameObject _newButterfly)
rContainer.transform.childCount);
            selectedButterfly.localScale = new Vector3(100 * transform.localScale.x * 0.5f, 100 * transform.localScale.x * 0.5f, 100 * transform.localScale.x * 0.5f);
            TextureMatchedRender(selectedButterfly.gameObject);
        }*/

    }

    void GetVariables()
    {
        rounds = 0;
        score = 0;
        gameState = 0;


        preHuntTime = CurrentConfig.conf.preHuntTime;
        huntTime = CurrentConfig.conf.huntTime;

        maximumKills = CurrentConfig.conf.maximumKills;
        minimumKills = CurrentConfig.conf.minimumKills;
        butterflyRoundSpawnAmount = CurrentConfig.conf.butterflyRoundSpawnAmount;
        healthAmount = CurrentConfig.conf.healthAmount;

        resetEverythingOnNextGen = CurrentConfig.conf.resetEverythingOnNextGen; //TODO: Why is this here? Either remove or fix. It is broken atm
        noSafeClick = CurrentConfig.conf.noSafeClick;
        keepButterAmount = CurrentConfig.conf.keepButterAmount;

        ButterlfyTotalStartAmnt = CurrentConfig.conf.initAmntOfButter;
        initAmntOfWhite = CurrentConfig.conf.initAmntOfWhite;
        initAmountOfMixed = CurrentConfig.conf.initAmntOfGray;
        initAmountOfDark = CurrentConfig.conf.initAmountOfDark;

        renderLerp = CurrentConfig.conf.renderLerp;
        renderPerlin = CurrentConfig.conf.renderPerlin;
        renderButterBackground = CurrentConfig.conf.renderButterBackground;
        geneMode = CurrentConfig.conf.geneMode;
    }

    void ResetVariables()
    {
        //Init variables
        gameState = 1;
        if (maximumKills == 0) { liveMaximumKills = butterContainer.GetComponent<Transform>().childCount; }
        else { liveMaximumKills = maximumKills; }
        GetComponent<SplashShifter>().ShowSplash(0, preHuntSplash);
        deadButterflies = new List<GameObject>();
        isPaused = false;
    }

    void PrepareGame()
    {
        //Spawns butterflys with specific genes.
        int[] initButterAmnt = { initAmntOfWhite, initAmountOfMixed, initAmountOfDark};

        for (int i = 0; i < initButterAmnt.Length; i++)
        {
            for (int j = 0; j < initButterAmnt[i]; j++)
            {
                GameObject animal = SpawnButterfly();
                GeneticManager.UpdateAnimalGene(animal.GetComponent<ButterflyBehaviour>().gene, butterflyGeneLength, i, geneMode);
                ApplyButterflyBlendin(animal, GeneticManager.BlendInCalc(animal.GetComponent<ButterflyBehaviour>().gene));
            }
        }

        //Spawns rest of population (or all of population, if no fixed amt was specified) with random genes.
        for (int i = 0; i < ButterlfyTotalStartAmnt - (initAmountOfDark + initAmountOfMixed + initAmntOfWhite); i++)
        {
            GameObject animal = SpawnButterfly();
            GeneticManager.UpdateAnimalGene(animal.GetComponent<ButterflyBehaviour>().gene, butterflyGeneLength, geneMode);
            ApplyButterflyBlendin(animal, GeneticManager.BlendInCalc(animal.GetComponent<ButterflyBehaviour>().gene));
        }

        statsLogList.Add(new StatSave() { populationAmount = butterContainer.transform.childCount, alleleData = butterContainer.GetComponent<ButterCollection>().GetAnimalAlleles(), phenotypeData = butterContainer.GetComponent<ButterCollection>().GetAnimalPhenotypes(renderButterBackground), domMode = geneMode});
    }

    Vector3 RandomButterPos(Quaternion newButterRotate)
    {
        Vector2 boardSize = new Vector2 (
            x: GetComponent<Renderer>().bounds.size.x,
            y: GetComponent<Renderer>().bounds.size.y
            );


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

        } while (!(nrOfLoops > 500 || noOverlap)); //Break for infinite loop
        if (!noOverlap) Debug.LogError("Could not find space for butterfly, or code is broken.");

        return new Vector3(newButterX, newButterY, newButterZ);
    }

    GameObject SpawnButterfly()
    {
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(1, 360));
        GameObject newButterfly = Instantiate(butterfly,                    //Prefab
                                      RandomButterPos(randomRotation),      //Random pos, without overlapp
                                      randomRotation);                      //Random rot. Needs to be pre-calculated for col detect

        newButterfly.transform.name = "Butterfly";
        newButterfly.transform.parent = butterContainer.transform;
        //newButterfly.transform.localScale = new Vector3(100 * transform.localScale.y * 0.5f, 100 * transform.localScale.y * 0.5f, 100 * transform.localScale.y * 0.5f); //OBS! is undead
        //butterContainer.GetComponent<ButterCollection>().ResizeAnimals(); //New resize code that also doesnt work

        //resize animals
        newButterfly.GetComponent<ButterflyBehaviour>().gameBoard = this.gameObject;
        newButterfly.GetComponent<ButterflyBehaviour>().gene = new Gene();

        return newButterfly;
    }

    void ApplyButterflyBlendin(GameObject butterfly, float blendIn)
    {
        Renderer butterRender = butterfly.GetComponent<Renderer>();

        TextureMatchedRender(butterfly);

        if (renderButterBackground)
        {
            butterRender.material.SetFloat("_LerpValue", blendIn);
            butterRender.material.SetFloat("_enablePerlin", renderPerlin);
        }

        else
        {
            butterRender.material.SetTexture("_SecondaryTex", null);
            butterRender.material.SetFloat("_enablePerlin", blendIn);
        }

        //RandomOffset
        butterRender.material.SetFloat("_offsetX", Random.Range(1, 100));
        butterRender.material.SetFloat("_offsetY", Random.Range(1, 100));
    }

    void TextureMatchedRender(GameObject _newButterfly)
    {

        try
        {
            string modelName = _newButterfly.GetComponent<MeshFilter>().sharedMesh.name;
            if (!PlayerPrefs.HasKey(GetVariable.GetKeyPrefix() + modelName)) TextureMatchManager.Reset();

            string[] tempData = PlayerPrefs.GetString(keyPrefix + modelName).Split(':');

            float butterMatchX = float.Parse(tempData[0]);
            float butterMatchY = float.Parse(tempData[1]);
            bool squareMatch = bool.Parse(tempData[2]);

            float squareX, squareY;

            if (squareMatch)
            {
                squareX = _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x;
                squareY = _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y;
            }

            else
            {
                squareX = 1;
                squareY = 1;
            }

            _newButterfly.GetComponent<Renderer>().material = animalMaterial;

            _newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", blendTexture);
            _newButterfly.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
                _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * _newButterfly.transform.localScale.x * butterMatchX * squareY,
                _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * _newButterfly.transform.localScale.y * butterMatchY * squareX));

            _newButterfly.GetComponent<Renderer>().material.SetTexture("_SecondaryTex", backgroundTexture);
            _newButterfly.GetComponent<Renderer>().material.SetTextureScale("_SecondaryTex", new Vector2(
                _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * _newButterfly.transform.localScale.x * butterMatchX * squareY,
                _newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * _newButterfly.transform.localScale.y * butterMatchY * squareX));
        }

        catch (System.Exception)
        {
            Debug.LogError("Could not load correct material for model. Try rendermode 0, or give texture a size to scale");
        }


    }

    void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            GetComponent<SplashShifter>().ShowSplash(0, pauseMenu);
        }
        else
        {
            Time.timeScale = 1;
            RandomizeAnimalPos();
            GetComponent<SplashShifter>().HideSplash();
        }
    }


    private void Update()
    {
        switch (gameState)
        {
            case 1:
                spriteOverlayMan.GetComponent<SpriteOverlay>().MakeClickSpriteUI(liveMaximumKills);
                if (TimmerManagment.Timmer(preHuntTime))
                {
                    GetComponent<SplashShifter>().HideSplash();
                    Countdown.SetK(GetHuntTime());
                    gameState = 2;
                }

                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = !isPaused;
                    PauseGame();
                }

                if (noSafeClick && Input.GetMouseButtonDown(0) && !isPaused)
                {
                    liveMaximumKills--;
                    spriteOverlayMan.GetComponent<SpriteOverlay>().RemoveKlick();
                }

                if (TimmerManagment.Timmer(GetHuntTime()))
                { //Checks if Timer is finished. Time is dependant on an exponential value,
                  //y=C*a^x. Time decreases the more rounds have passed.
                  //postGameSplash.GetComponent<Canvas>().enabled = true;

                    if (butterContainer.transform.childCount == 0)
                    {
                        SoundScript.SetVariables(rounds, butterflyGeneLength + 1, statsLogList, score);
                        GetComponent<LoadSceneFunctions>().ToGraph();
                        gameState = 4;//Failed! Health will be lost, energy will be lost or game will be lost here.
                    }

                    else if (deadButterContainer.GetComponent<Transform>().childCount < minimumKills)
                    {
                        healthAmount--;
                        spriteOverlayMan.GetComponent<SpriteOverlay>().RemoveHeart();

                        switch (healthAmount)
                        {
                            case 0:
                                //GetComponent<SplashShifter>().ShowSplash(0, gameOverSplash);
                                Debug.Log("U loose");
                                SoundScript.SetVariables(rounds, butterflyGeneLength + 1, statsLogList, score);
                                GetComponent<LoadSceneFunctions>().ToGraph();
                                gameState = 4;//Failed! Health will be lost, energy will be lost or game will be lost here.
                                break;

                            default:
                                GetComponent<SplashShifter>().ShowSplash(0, preHuntSplash);
                                Debug.Log("U loose heart");
                                gameState = 3;
                                break;
                        }
                    }

                    else
                    {
                        gameState = 3;
                    }

                }

                break;

            case 3:
                rounds++;
                for (int i = 0; i < deadButterflies.Count; i++)
                {
                    Destroy(deadButterflies[i].gameObject);
                }
                Debug.Log("Score: " + score);

                Debug.Log("continu");

                RandomizeAnimalPos();

                if (keepButterAmount)
                {
                    butterflyRoundSpawnAmount = deadButterContainer.transform.childCount;
                }

                for (int i = 0; i < butterflyRoundSpawnAmount; i++)
                {
                    GameObject animal = SpawnButterfly();
                    GeneticManager.UpdateAnimalGene(animal.GetComponent<ButterflyBehaviour>().gene,
                                                    butterContainer.GetComponent<ButterCollection>().GetAnimalGenes(),
                                                    geneMode);

                    ApplyButterflyBlendin(animal, GeneticManager.BlendInCalc(animal.GetComponent<ButterflyBehaviour>().gene));
                }

                ResetVariables();

                if (maximumKills == 0)
                {
                    liveMaximumKills = butterContainer.GetComponent<Transform>().childCount;
                    //liveMaximumKills = butterfliesRemaining;
                }

                statsLogList.Add(new StatSave() { populationAmount = butterContainer.transform.childCount, alleleData = butterContainer.GetComponent<ButterCollection>().GetAnimalAlleles(), phenotypeData = butterContainer.GetComponent<ButterCollection>().GetAnimalPhenotypes(renderButterBackground), domMode = geneMode });

                break;

            default:
                break;
        }
    }

    public void RandomizeAnimalPos()
    {
        //Move outside box. This will keep auto-col detect from triggering
        foreach (Transform animal in butterContainer.transform)
        {
            animal.transform.position = new Vector3(0, 0, 5);
        }

        //Randomize pos and rot
        for (int i = 0; i < butterContainer.transform.childCount; i++)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, Random.Range(1, 360));
            Vector3 newPos = RandomButterPos(newRot);

            butterContainer.transform.GetChild(i).transform.position = newPos;
            butterContainer.transform.GetChild(i).rotation = newRot;
        }

    }

    void ClearBoard()
    {
        foreach (Transform butterfly in butterContainer.GetComponentInChildren<Transform>())
        {
            Destroy(butterfly.gameObject);
        }
    }

    public void SetScore()
    {
        float remainingTime = TimmerManagment.GetTimeLeft();
        score += Mathf.CeilToInt(10f * remainingTime / (GetHuntTime() - remainingTime));
    }

    public int GetScore()
    {
        return score;
    }

    public int GetRounds()
    {
        return rounds;
    }

    public float GetHuntTime()
    {
        if (huntTime * Mathf.Pow(huntTimeReducePercent, rounds) > huntTimeMin)
        {
            return huntTime * Mathf.Pow(huntTimeReducePercent, rounds);
        }
        else
        {
            return huntTimeMin;
        }
    }

    /*
    |=============================|
    |==BUTTERFLY EVENT HANDELERS==|
    |=============================|
    */

    public void ButterClick(GameObject butterfly, AudioClip audioClip)
    {
        if (gameState == 2 && (liveMaximumKills > 0) && !isPaused)
        {
            if (butterfly.transform.parent.name == "ButterCollection")
            {
                SoundScript.PlayAudio(audioClip);
            }
            SetScore();
            butterfly.transform.parent = deadButterContainer.transform;
            butterfly.GetComponent<MeshCollider>().enabled = false;
            butterfly.GetComponent<MeshFilter>().mesh = destroyedButterfly;
            deadButterflies.Add(butterfly);
            if (!noSafeClick) { spriteOverlayMan.GetComponent<SpriteOverlay>().RemoveKlick(); liveMaximumKills--; }
            Debug.Log("Butterfly click detected: Removed " + butterfly.name + " from the game board");
        }
    }
}
