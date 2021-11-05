using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButterHuntVariables 
{
    static public float preHuntTime, huntTime;
    static public int butterflyGeneLength, butterflyStartAmountRandom, butterflyStartAmountGene, maximumKills, minimumKills, butterflyRenderMode, butterflyRoundSpawnAmount;
    static public bool resetEverythingOnNextGen;

    static public void SetMode(int mode)
    {
        switch (mode) {
            case 1: //Easy
                preHuntTime = 10f;
                huntTime = 10f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 2;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 1;
                butterflyRoundSpawnAmount = 2;

                resetEverythingOnNextGen = true;
                break;

            case 2: //Medium
                preHuntTime = 10f;
                huntTime = 10f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 2;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 1;
                butterflyRoundSpawnAmount = 2;

                resetEverythingOnNextGen = true;
                break;


            case 3: //Hard
                preHuntTime = 10f;
                huntTime = 10f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 2;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 1;
                butterflyRoundSpawnAmount = 2;

                resetEverythingOnNextGen = true;
                break;

        }
    }
}
