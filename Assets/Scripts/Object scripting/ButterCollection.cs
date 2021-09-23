using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterCollection : MonoBehaviour
{
    public bool[][] GetAnimalGenes()
    {
        int nrOfAnimals = transform.childCount;
        bool[][] animalGenes = new bool[nrOfAnimals][];

        for (int i = 0; i < transform.childCount; i++)
        {
            animalGenes[i] = transform.GetChild(i).GetComponent<ButterflyBehaviour>().genes;
        }

        return animalGenes;
    }
}
