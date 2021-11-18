using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButterHuntVariables 
{
    static public float preHuntTime, huntTime;
    static public int butterflyGeneLength, butterflyStartAmountRandom, butterflyStartAmountGene, maximumKills, minimumKills, butterflyRenderMode, butterflyRoundSpawnAmount, healthAmount;
    static public bool resetEverythingOnNextGen;

    static public void SetMode(int mode)
    {
        switch (mode) {
            case 1: //Easy
                preHuntTime = 1.5f;
                huntTime = 3f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 8;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 1;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 7;

                resetEverythingOnNextGen = true;
                break;

            case 2: //Medium
                preHuntTime = 1f;
                huntTime = 3f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 6;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 2;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 3;

                resetEverythingOnNextGen = true;
                break;


            case 3: //Hard
                preHuntTime = 0.5f;
                huntTime = 4f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 4;
                butterflyStartAmountGene = 1;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRenderMode = 3;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 1;

                resetEverythingOnNextGen = true;
                break;

        }
    }
}
