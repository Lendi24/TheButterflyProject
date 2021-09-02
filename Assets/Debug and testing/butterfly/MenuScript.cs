using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        EditorSceneManager.LoadScene("SpawnTest");
    }
}
