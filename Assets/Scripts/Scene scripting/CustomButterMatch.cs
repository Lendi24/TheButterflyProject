using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CustomButterMatch : MonoBehaviour
{
    [SerializeField]
    string modelName;

    [SerializeField]
    float tilesPerUnit, butterMatchX, butterMatchY, butterTransX, butterTransY, butterTransRot;

    [SerializeField]
    bool snapMatchYtoX, squareMatch;

    [SerializeField]
    Texture2D backgroundTexture, blendTexture;

    [SerializeField]
    Material backgroundMaterial;

    [SerializeField]
    GameObject butterfly;
    GameObject newButterfly;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = backgroundMaterial;
        GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit,
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));


        float newButterZ = ((butterfly.GetComponent<Renderer>().bounds.size.z) / -2);
        newButterfly = Instantiate(butterfly, new Vector3(
        butterTransX, butterTransY, newButterZ), Quaternion.Euler(0, 0, butterTransRot));

        modelName = newButterfly.GetComponent<MeshFilter>().sharedMesh.name;
        Load();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (snapMatchYtoX)
        {
            butterMatchY = butterMatchX;
        }

        newButterfly.transform.position = new Vector2(0 + butterTransX, 0 + butterTransY);

        newButterfly.GetComponent<Renderer>().material = backgroundMaterial;
        newButterfly.GetComponent<Renderer>().material.SetTexture("_MainTex", backgroundTexture);
        newButterfly.GetComponent<Renderer>().material.SetTexture("__SecondaryTex", blendTexture);
        newButterfly.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.x * tilesPerUnit * newButterfly.transform.localScale.x * butterMatchX * squareY,
            newButterfly.GetComponent<MeshFilter>().mesh.bounds.size.y * tilesPerUnit * newButterfly.transform.localScale.y * butterMatchY * squareX));

        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(
            GetComponent<Renderer>().bounds.size.x * tilesPerUnit,
            GetComponent<Renderer>().bounds.size.y * tilesPerUnit));

    }

    public void Save()
    {
        TextureMatchManager.Save(modelName, butterMatchX, butterMatchY, squareMatch);
    }

    public void Load()
    {
        ModelTextureMap loadedObject = TextureMatchManager.Load(modelName);

        snapMatchYtoX = (loadedObject.butterMatchX == loadedObject.butterMatchY);
        squareMatch = loadedObject.squareMatch;

        butterMatchX = loadedObject.butterMatchX;
        butterMatchY = loadedObject.butterMatchY;
    }

    public void delete()
    {
        TextureMatchManager.Delete(modelName);
    }

    public void reset()
    {
        TextureMatchManager.Reset();
        Load();
    }
}
