using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButterHuntVariables 
{
    static public float preHuntTime, huntTime, renderLerp, renderPerlin;
    static public int butterflyGeneLength, butterflyStartAmountRandom, butterflyStartAmountGene, maximumKills, minimumKills, butterflyRoundSpawnAmount, healthAmount;
    static public bool resetEverythingOnNextGen, noSafeClick, keepButterAmount, renderButterBackground;

    static public void SetMode(int mode)
    {
        switch (mode) {
            case 1: //Easy
                preHuntTime = 1f;
                huntTime = 3f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 8;
                butterflyStartAmountGene = 2;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 3;

                resetEverythingOnNextGen = true;
                keepButterAmount = false;
                noSafeClick = true;

                renderLerp = 1;
                renderPerlin = 0;
                renderButterBackground = true;

                break;

            case 2: //Medium
                preHuntTime = 1f;
                huntTime = 4f;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 8;
                butterflyStartAmountGene = 2;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 2;

                resetEverythingOnNextGen = true;
                keepButterAmount = false;
                noSafeClick = true;

                renderLerp = 1;
                renderPerlin = 1;
                renderButterBackground = true;

                break;


            case 3: //Hard
                preHuntTime = 1f;
                huntTime = 5f;
                noSafeClick = true;

                butterflyGeneLength = 3;
                butterflyStartAmountRandom = 8;
                butterflyStartAmountGene = 2;
                maximumKills = 3;
                minimumKills = 1;
                butterflyRoundSpawnAmount = 2;
                healthAmount = 1;

                resetEverythingOnNextGen = true;
                keepButterAmount = false;
                noSafeClick = true;

                renderLerp = 0;
                renderPerlin = 1;
                renderButterBackground = false;

                break;

        }
    }

    static public void SetCustom(float _preHuntTime, float _huntTime, int _butterflyGeneLength, int _butterflyStartAmountRandom, int _butterflyStartAmountGene, int _maximumKills, int _minimumKills, int _butterflyRoundSpawnAmount, int _healthAmount, bool _resetEverythingOnNextGen, bool _noSafeClick, bool _keepButterAmount, float _renderLerp, float _renderPerlin, bool _renderButterBackground)
    {
        preHuntTime = _preHuntTime;
        huntTime = _huntTime;
        noSafeClick = _noSafeClick;

        butterflyGeneLength = _butterflyGeneLength;
        butterflyStartAmountRandom = _butterflyStartAmountRandom;
        butterflyStartAmountGene = _butterflyStartAmountGene;
        maximumKills = _maximumKills;
        minimumKills = _minimumKills;
        //butterflyRenderMode = _butterflyRenderMode;
        butterflyRoundSpawnAmount = _butterflyRoundSpawnAmount;
        healthAmount = _healthAmount;

        resetEverythingOnNextGen = _resetEverythingOnNextGen;
        keepButterAmount = _keepButterAmount;
        noSafeClick = _noSafeClick;

        //Render mode is replaced by the two variables below:
        renderLerp = _renderLerp;
        renderPerlin = _renderPerlin;
        renderButterBackground = _renderButterBackground;
    }
}
