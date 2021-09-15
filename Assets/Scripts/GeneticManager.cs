using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneticManager : MonoBehaviour
{
    public static bool[] GiveGenetics(int geneticLength)
    {
        bool[] genetics = new bool[geneticLength];
        for (int i = 0; i < geneticLength; i++)
        {
            genetics[i] = Random.Range(0f, 1f) > 0.5;
        }

        return SortBoolArray(genetics);
    }

    static bool[] SortBoolArray(bool[]boolArray)
    {
        bool swapped = true;
        bool done = false;

        while (!done)
        {
            done = true;

            if (swapped)
            {
                for (int i = 0; i < boolArray.Length - 1; i++)
                {
                    if (!boolArray[i] && boolArray[i + 1])
                    {
                        bool temp = boolArray[i];
                        boolArray[i] = boolArray[i + 1];
                        boolArray[i + 1] = temp;
                        done = false;
                    }
                }
            }

            else
            {
                int i = boolArray.Length - 1;
                for (; i >= 1; i--)
                {
                    if (boolArray[i] && !boolArray[i - 1])
                    {
                        bool temp = boolArray[i];
                        boolArray[i] = boolArray[i - 1];
                        boolArray[i - 1] = temp;
                        done = false;
                    }
                }
            }

        }

        return boolArray;
    }

    public static float BlendInCalc(bool[] genetics)
    {

        return 1;
    } 
}
