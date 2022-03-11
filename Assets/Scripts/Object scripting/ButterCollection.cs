using System;
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

    public float[] GetAnimalPhenotypes()
    {
        int nrOfAnimals = transform.childCount;
        float[] animalPhenotypes = new float[nrOfAnimals];
        
        for(int i = 0; i < transform.childCount; i++)
        {
            animalPhenotypes[i] = transform.GetChild(i).GetComponent<Renderer>().material.GetFloat("_LerpValue");
        }

        return new List<float>(animalPhenotypes).ToArray();
    }

    public void ResizeAnimals (float xSize)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform animal = transform.GetChild(i).GetComponent<Transform>();
            float ratio = animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
            float newScaleX = (xSize/ animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.x);
            float newScaleY = (xSize * ratio / animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.y);
            animal.localScale = new Vector2(newScaleX, newScaleY);
        }
    }

    public void ResizeAnimals()
    {
        Transform animal = transform.GetChild(0).GetComponent<Transform>();
        float ratio = animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / animal.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
        float newX = Mathf.Sqrt(
            ((100*GameBoardResizer.GetGameBoardSize().x * GameBoardResizer.GetGameBoardSize().z) / (Mathf.Abs(65-transform.childCount) + 10))/ratio);
        ResizeAnimals(newX);
    }
}

