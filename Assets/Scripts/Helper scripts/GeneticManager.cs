using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneticManager : MonoBehaviour
{
    static int lastSpawnedTrueGeneLen;

    public static bool[] GiveSpecificGenetics(int geneticLength, int trueGenes)
    {
        bool[] genes = new bool[geneticLength];

        for (int i = 0; i < trueGenes; i++)
        {
            genes[i] = true;
        }

        return genes;
    }

    public static bool[] GiveRandomGenetics(int geneticLength)
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
        float geneTrueLength = 0; //this should be float. Otherwise it breaks when dividing, since an int will be returned. Don't change!

        for (int i = 0; i < genetics.Length && genetics[i]; i++)
        {
            geneTrueLength++;
        }

        return geneTrueLength / genetics.Length;
    }
    
    public static bool[] EvolveNewAnimal(bool[][] animalsInPlay, int butterflyGeneLength)
    {
        int newTrueGeneLength;

        if (animalsInPlay.Length <= 0)
        {
            newTrueGeneLength = lastSpawnedTrueGeneLen;
        }

        else
        {
            int geneTrueLength = 0;
            for (int i = 0; i < animalsInPlay.Length; i++)
            {
                for (int j = 0; j < animalsInPlay[i].Length && animalsInPlay[i][j]; j++)
                {
                    geneTrueLength++;
                }
            }

            newTrueGeneLength = geneTrueLength / animalsInPlay.Length;
            if (lastSpawnedTrueGeneLen == newTrueGeneLength) newTrueGeneLength += Mutation(-1, 1);
            lastSpawnedTrueGeneLen = newTrueGeneLength;
        }

        bool[] newGenes = new bool[butterflyGeneLength];
        for (int i = 0; i < newGenes.Length; i++)
        {
            newGenes[i] = i < newTrueGeneLength;
        }

        return newGenes;
    }

    public static int Mutation(int low, int max)
    {
        if (Random.Range(0,15) == 0)
        {
            return Random.Range(low, max + 1);
        }
        return 0;
    }
}
