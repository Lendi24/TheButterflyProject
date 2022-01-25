using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetVariable
{
    public static string GetKeyPrefix()
    {
        return "modelMatch";
    }
}

public class ModelTextureMap{
    public string modelName;
    public bool squareMatch;
    public float butterMatchX;
    public float butterMatchY;

    public ModelTextureMap(string _modelName, bool _squareMatch, float _butterMatchX, float _butterMatchY)
    {
        modelName = _modelName;
        squareMatch = _squareMatch;
        butterMatchX = _butterMatchX;
        butterMatchY = _butterMatchY;
    }
}

public class TextureMatchManager : MonoBehaviour
{

    public static void Save(string modelName, float butterMatchX, float butterMatchY, bool squareMatch)
    {
        if (CanSave(modelName, butterMatchX, butterMatchY))
        {
            PlayerPrefs.SetString(GetVariable.GetKeyPrefix() + modelName, butterMatchX + ":" + butterMatchY + ":" + squareMatch);
            Debug.Log("Saving model '" + modelName + "', with MatchX:" + butterMatchX + ", MatchY:" + butterMatchY + " and squareMatch set to " + squareMatch);
        }
        else { Debug.LogError("Could not save! Is name empty? Are you trying to overwrite an already existing key?"); }
    }

    public static ModelTextureMap Load(string modelName)
    {
        if (PlayerPrefs.HasKey(GetVariable.GetKeyPrefix() + modelName))
        {
            string[] tempData = PlayerPrefs.GetString(GetVariable.GetKeyPrefix() + modelName).Split(':');

            //bool snapMatchYtoX = (tempData[0] == tempData[1]);
            bool squareMatch = bool.Parse(tempData[2]);
            float butterMatchX = float.Parse(tempData[0]);
            float butterMatchY = float.Parse(tempData[1]);

            ModelTextureMap loadedObject = new ModelTextureMap(_modelName: modelName,
                                                               _squareMatch: squareMatch,
                                                               _butterMatchX: butterMatchX,
                                                               _butterMatchY: butterMatchY);

            Debug.Log("Loading model '" + modelName + "', with MatchX:" + butterMatchX + ", MatchY:" + butterMatchY + " and squareMatch set to " + squareMatch);
            return loadedObject;
        }
        else { Debug.LogError("A model named '" + modelName + "' does not exist"); }

        return null;
    }

    public static bool CanSave(string modelName, float butterMatchX, float butterMatchY)
    {
        bool canSave = true;
        //Can not save if:
        if ((modelName == "") ||                                         //name does not exist
        (PlayerPrefs.HasKey(GetVariable.GetKeyPrefix() + modelName)) ||                     //key with same name exists
        (butterMatchX == 0 || butterMatchY == 0)) canSave = false;  //Saved values are 0    

        return canSave;
    }

    public static void Delete(string modelName)
    {
        if (PlayerPrefs.HasKey(GetVariable.GetKeyPrefix() + modelName))
        {
            Load(modelName);
            Debug.Log("Removing " + modelName);
            PlayerPrefs.DeleteKey(GetVariable.GetKeyPrefix() + modelName);
        }
        else { Debug.LogError("Model '" + modelName + "' does not exist!"); }
    }

    public static void Reset()
    {
        PlayerPrefs.SetString(GetVariable.GetKeyPrefix() + "Classic Butterfly", 117 + ":" + 117 + ":" + true);
        PlayerPrefs.SetString(GetVariable.GetKeyPrefix() + "ClassicButterfly", 117 + ":" + 117 + ":" + true);
        PlayerPrefs.SetString(GetVariable.GetKeyPrefix() + "Butterfly", 117 + ":" + 117 + ":" + true);

    }
}
