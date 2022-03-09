using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CurrentConfig
{
    public static ConfigurationSettings conf;

    public static string SAVE_FOLDER = Application.dataPath + "/ConfigFiles/";
    public static string SAVE_PREFIX = "config-";
    public static string SAVE_EXT = "BtrConf";

    public static string FILE_PATH = SAVE_FOLDER + SAVE_PREFIX + "{0}" + "." + SAVE_EXT;
}

public class ConfigurationSettings
{
    public float preHuntTime;
    public float huntTime;
    public int   butterflyGeneLength;
    public int   butterflyStartAmountRandom;
    public int   butterflyStartAmountGene;
    public int   maximumKills;
    public int   minimumKills;
    public int   butterflyRoundSpawnAmount;
    public int   healthAmount;
    public bool  resetEverythingOnNextGen;
    public bool  noSafeClick;
    public bool  keepButterAmount;
    public float renderLerp;
    public float renderPerlin;
    public bool  renderButterBackground;
    public bool?  geneMode;

}

public static class ConfigurationFunctions 
{

    public static void ApplyConfig(ConfigurationSettings _conf)
    {
        CurrentConfig.conf = _conf;
    }


    public static ConfigurationSettings MakeConfObject(
        float   _preHuntTime,
        float   _huntTime,
        int     _butterflyGeneLength,
        int     _butterflyStartAmountRandom,
        int     _butterflyStartAmountGene,
        int     _maximumKills,
        int     _minimumKills,
        int     _butterflyRoundSpawnAmount,
        int     _healthAmount,
        bool    _resetEverythingOnNextGen,
        bool    _noSafeClick,
        bool    _keepButterAmount,
        float   _renderLerp,
        float   _renderPerlin,
        bool    _renderButterBackground,
        bool?   _geneMode
        )
    {

        ConfigurationSettings conf = new()
        {
            preHuntTime                 = _preHuntTime,
            huntTime                    = _huntTime,
            butterflyGeneLength         = _butterflyGeneLength,
            butterflyStartAmountRandom  = _butterflyStartAmountRandom,
            butterflyStartAmountGene    = _butterflyStartAmountGene,
            maximumKills                = _maximumKills,
            minimumKills                = _minimumKills,
            butterflyRoundSpawnAmount   = _butterflyRoundSpawnAmount,
            healthAmount                = _healthAmount,
            resetEverythingOnNextGen    = _resetEverythingOnNextGen,
            noSafeClick                 = _noSafeClick,
            keepButterAmount            = _keepButterAmount,
            renderLerp                  = _renderLerp,
            renderPerlin                = _renderPerlin,
            renderButterBackground      = _renderButterBackground,
            geneMode                    = _geneMode
        };

        return conf;
    }

    public static void SaveToFile(ConfigurationSettings conf, string FILE_NAME)
    {
        string filePath = CurrentConfig.SAVE_FOLDER;
        string fileName = CurrentConfig.SAVE_PREFIX + FILE_NAME + "." + CurrentConfig.SAVE_EXT;

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        File.WriteAllText(filePath + fileName, JsonUtility.ToJson(conf));
        Debug.Log(CurrentConfig.SAVE_FOLDER + CurrentConfig.SAVE_PREFIX + FILE_NAME + "." + CurrentConfig.SAVE_EXT);
    }

    public static void LoadFromFile(string FILE_NAME)
    {
        /*
        DirectoryInfo directoryInfo = new DirectoryInfo(CurrentConfig.SAVE_FOLDER);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + CurrentConfig.SAVE_SULFIX);
        // Cycle through all save files and identify the most recent one
        */
        ConfigurationSettings saveObject = JsonUtility.FromJson<ConfigurationSettings>(CurrentConfig.SAVE_FOLDER + CurrentConfig.SAVE_PREFIX + FILE_NAME + "." + CurrentConfig.SAVE_EXT);
    }
}
