using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CurrentConfig
{
    public static ConfigurationSettings conf;

    public static string SAVE_PATH = Application.persistentDataPath + "/ConfigFiles/";
    public static string SAVE_EXT = "BtrConf";
}

public class ConfigurationSettings
{
    public string   confName;
    public float    preHuntTime;
    public float    huntTime;
    public int      initAmntOfWhite;
    public int      initAmntOfGray;
    public int      initAmntOfButter;
    public int      initAmountOfDark;
    public int      maximumKills;
    public int      minimumKills; 
    public int      butterflyRoundSpawnAmount;
    public int      healthAmount;
    public bool     resetEverythingOnNextGen;
    public bool     noSafeClick;
    public bool     keepButterAmount;
    public float    renderLerp;
    public float    renderPerlin;
    public bool     renderButterBackground;
    public int      geneMode;

}

public static class ConfigurationFunctions 
{

    public static void ApplyConfig(ConfigurationSettings _conf)
    {
        CurrentConfig.conf = _conf;
    }


    public static ConfigurationSettings MakeConfObject(
        string  _confName,
        float   _preHuntTime,
        float   _huntTime,
        int     _initAmntOfButter,
        int     _initAmntOfWhite,
        int     _initAmntOfGray,
        int     _initAmountOfDark,
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
        int     _geneMode
        )
    {

        ConfigurationSettings conf = new()
        {
            confName                    = _confName,
            preHuntTime                 = _preHuntTime,
            huntTime                    = _huntTime,
            initAmntOfButter            = _initAmntOfButter,
            initAmntOfWhite             = _initAmntOfWhite,
            initAmntOfGray              = _initAmntOfGray,
            initAmountOfDark            = _initAmountOfDark,
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

    public static void InitFolder()
    {
        DirectoryInfo dir = new DirectoryInfo(CurrentConfig.SAVE_PATH);

        foreach (FileInfo file in dir.GetFiles())
        {
            file.Delete();
        }

        SaveToFile(ConfigurationFunctions.MakeConfObject(
            _confName: "ClassicE",
            _preHuntTime: 1f,
            _huntTime: 3f,

            _initAmntOfButter: 8,
            _initAmntOfWhite: 1,
            _initAmntOfGray: 1,
            _initAmountOfDark: 1,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 3,

            _resetEverythingOnNextGen: true,
            _noSafeClick: false,
            _keepButterAmount: false,

            _renderLerp: 1,
            _renderPerlin: 0,
            _renderButterBackground: true,

            _geneMode: 2), "1ClassicE");

        SaveToFile(ConfigurationFunctions.MakeConfObject(
            _confName: "ClassicM",
            _preHuntTime: 1f,
            _huntTime: 4f,

            _initAmntOfButter: 8,
            _initAmntOfWhite: 2,
            _initAmntOfGray: 2,
            _initAmountOfDark: 2,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 2,

            _resetEverythingOnNextGen: true,
            _noSafeClick: true,
            _keepButterAmount: false,

            _renderLerp: 1,
            _renderPerlin: 1,
            _renderButterBackground: true,

            _geneMode: 0), "2ClassicM");

        SaveToFile(ConfigurationFunctions.MakeConfObject(
            _confName: "ClassicH",
            _preHuntTime: 1f,
            _huntTime: 5f,

            _initAmntOfButter: 8,
            _initAmntOfWhite: 2,
            _initAmntOfGray: 2,
            _initAmountOfDark: 2,
            _maximumKills: 3,
            _minimumKills: 1,
            _butterflyRoundSpawnAmount: 2,
            _healthAmount: 3,

            _resetEverythingOnNextGen: true,
            _noSafeClick: true,
            _keepButterAmount: false,

            _renderLerp: 0,
            _renderPerlin: 1,
            _renderButterBackground: false,

            _geneMode: 1), "3ClassicH");
    }

    public static void RemoveFile(string FILE_NAME)
    {
        string fileFullPath = CurrentConfig.SAVE_PATH + FILE_NAME + "." + CurrentConfig.SAVE_EXT;
        if (Directory.Exists(CurrentConfig.SAVE_PATH))
        {
            File.Delete(fileFullPath);
        }
    }

    public static string GetValidFilenameFromName(string name, int num)
    {
        string newFileName = name;
        if (num != 0) { newFileName = newFileName + " " + num;  }

        foreach (var existingFileName in GetConfigFiles())
        {
            if (newFileName == existingFileName)
            {
                if (int.TryParse(existingFileName[existingFileName.Length - 1].ToString(), out int detectedNum))
                {
                    newFileName = GetValidFilenameFromName(name, detectedNum + 1);
                }

                else
                {
                    newFileName = GetValidFilenameFromName(name, 1);
                }

                break;
            }
        }

        return newFileName;
    }

    public static void SaveToFile(ConfigurationSettings conf, string FILE_NAME)
    {
        string filePath = CurrentConfig.SAVE_PATH;
        string fileName = FILE_NAME + "." + CurrentConfig.SAVE_EXT;

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        File.WriteAllText(filePath + fileName, JsonUtility.ToJson(conf));
    }

    public static string[] GetConfigFiles()
    {
        string filePath = CurrentConfig.SAVE_PATH;
        string[] saveFileNames;

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            saveFileNames = new string[0];
        }

        else
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            FileInfo[] saveFilesInfo = directoryInfo.GetFiles("*." + CurrentConfig.SAVE_EXT);
            saveFileNames = new string[saveFilesInfo.Length];

            for (int i = 0; i < saveFilesInfo.Length; i++)
            {
                saveFileNames[i] = saveFilesInfo[i].Name.Replace(saveFilesInfo[i].Extension, "");
            }
        }

        return saveFileNames;
    }

    public static ConfigurationSettings LoadFromFile(string FILE_NAME)
    {
        string filePath = CurrentConfig.SAVE_PATH;
        string fileName = FILE_NAME + "." + CurrentConfig.SAVE_EXT;
        string jsonData = File.ReadAllText(filePath + fileName);
        return JsonUtility.FromJson<ConfigurationSettings>(jsonData);
    }
}
