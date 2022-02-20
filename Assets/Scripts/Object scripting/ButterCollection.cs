using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterCollection : MonoBehaviour
{
    public Gene[] GetAnimalGenes()
    {
        int nrOfAnimals = transform.childCount;
        Gene[] animalGenes = new Gene[nrOfAnimals];

        for (int i = 0; i < transform.childCount; i++)
        {
            animalGenes[i] = transform.GetChild(i).GetComponent<ButterflyBehaviour>().gene;
        }

        return new List<Gene>(animalGenes).ToArray();
    }

    public bool[][] GetAnimalAlleles()
    {
        int nrOfAnimals = transform.childCount;
        bool[][] animalGenes = new bool[nrOfAnimals][];

        for (int i = 0; i < transform.childCount; i++)
        {
            animalGenes[i] = transform.GetChild(i).GetComponent<ButterflyBehaviour>().gene.alleles;
        }

        return new List<bool[]>(animalGenes).ToArray();
    }
}

