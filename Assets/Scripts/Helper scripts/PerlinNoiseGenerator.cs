using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGenerator : MonoBehaviour
{
    public static Texture2D GenerateCustomNoise(int width, int height, int offsetX, int offsetY, float scale, float balance)
    {
        Texture2D texture = new Texture2D(width, height);
        Color color;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                if (sample >= balance)
                {
                    color = new Color(Mathf.Ceil(sample), Mathf.Ceil(sample), Mathf.Ceil(sample));
                }
                else
                {
                    color = new Color(Mathf.Floor(sample), Mathf.Floor(sample), Mathf.Floor(sample));
                }

                texture.SetPixel(x, y, color);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    public static Texture2D GenerateDefault(int w, int h)
    {
        return GenerateCustomNoise(
            width:      w * 10,
            height:     h * 10,
            offsetX:    1,
            offsetY:    1,
            scale:      10,
            balance:    0.75f
            );
    }
}
