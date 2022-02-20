using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneticManager : MonoBehaviour
{
    public static void UpdateAnimalGene (Gene animalGene, int allelLen, int trueLen)
    {
        bool[] alleles = new bool[allelLen];

        for (int i = 0; i < trueLen; i++)
        {
            alleles[i] = true;
        }

        animalGene.alleles = alleles;
    }

    public static void UpdateAnimalGene(Gene animalGene, int allelLen)
    {
        UpdateAnimalGene(animalGene, allelLen, Random.Range(0,allelLen+1));
    }

    public static void UpdateAnimalGene(Gene animalGene, Gene[] allgenes)
    { 
        Gene firstGeneRef = PickRandomGene(allgenes);
        Gene SecondGeneRef = PickRandomGene(allgenes);
        Gene NewGene = CombineAlleles(firstGeneRef, SecondGeneRef);

        UpdateAnimalGene(animalGene, allgenes[0].alleles.Length, GetTrueLen(NewGene.alleles));
    }

    public static float BlendInCalc(Gene animalGene)
    {
        float trueLen = 0f; //this should be float. Otherwise it breaks when dividing, since an int will be returned. Don't change!

        for (int i = 0; i < animalGene.alleles.Length && animalGene.alleles[i]; i++)
        {
            trueLen++;
        }
        
        return trueLen / animalGene.alleles.Length;
    }

    public static Gene PickRandomGene(Gene[] allGenes)
    {
        int randIndex = Random.Range(0, allGenes.Length - 1); //Last one will be just created, and will have null as value
        Gene gene = allGenes[randIndex];
        return gene;
    }

    public static Gene CombineAlleles(Gene firstAllelsRef, Gene SecondAllelsRef) //Can be redone to take lists and handle data with for-loops for more dynamic code
    {
        if (firstAllelsRef.alleles.Length != SecondAllelsRef.alleles.Length)
        {
            Debug.LogError("Genetics Error! Tried to match two different sized genes");
        }

        int firstTrueLen = GetTrueLen(firstAllelsRef.alleles);
        int SecondTrueLen = GetTrueLen(SecondAllelsRef.alleles);
        int maxLen = firstAllelsRef.alleles.Length;

        bool firstAllel;
        if (firstTrueLen == maxLen || firstTrueLen == 0)
        {
            firstAllel = firstTrueLen == maxLen;
        }

        else 
        {
            firstAllel = Random.value > 0.5;
        }
        //Yeah.. this is bad code... Change it when we have time

        bool SecondAllel;
        if (firstTrueLen == maxLen || firstTrueLen == 0)
        {
            SecondAllel = SecondTrueLen == maxLen;
        }

        else
        {
            SecondAllel = Random.value > 0.5;
        }

        if (!firstAllel && SecondAllel)
        {
            firstAllel = true;
            SecondAllel = false;
        }

        Gene newGene = new Gene { alleles = new bool[2] };
        newGene.alleles[0] = firstAllel;
        newGene.alleles[1] = SecondAllel;

        return newGene;
    }

    public static int GetTrueLen(bool[] alleles)
    {
        int i = 0;
        while (i < alleles.Length && alleles[i])
        {
            i++;
        }
        return i;
    }
}
