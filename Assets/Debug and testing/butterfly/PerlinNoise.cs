using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //renderer.material.mainTexture = GenerateTexture();
    }

    public Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if(sample >= 0.5f)
        {
            return new Color(Mathf.Ceil(sample), Mathf.Ceil(sample), Mathf.Ceil(sample));
        }
        else 
        {
            return new Color(Mathf.Floor(sample), Mathf.Floor(sample), Mathf.Floor(sample));
        }

        //return new Color(sample, sample, sample);

    }
}
