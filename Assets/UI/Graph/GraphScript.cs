using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{
    public Material mat;
    public GameObject graphBoard;
    public int timeValue;
    public int maxValue;
    public int geneAmount;
    //LineRenderer lineRenderer;
    Vector3[] vertices;
    //Vector2[] uv;
    Vector3 origo;
    int[] values;
    int[,] genesArr;
    int[] triangles;
    Color[] graphColors;
    // Start is called before the first frame update
    void Start()
    {
        values = new int[timeValue+1];
        vertices = new Vector3[(timeValue+1)*2];
        genesArr = new int[geneAmount, timeValue + 1];
        //int[] triangles = new int[timeValue+3];

        SetColors();
        RandomizeValues();
        
        origo = new Vector3(graphBoard.transform.position.x - (graphBoard.transform.localScale.x / 2), graphBoard.transform.position.y - (graphBoard.transform.localScale.y / 2), 0);
        triangles = new int[(timeValue + 1) * 6];

        CreateMeshes();

        /*GameObject line = new GameObject();
        line.name = "Line";
        line.AddComponent<LineRenderer>();
        line.transform.parent = graphBoard.transform;*/
        /*lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = mat;
        lineRenderer.startWidth = 0.16f; 
        lineRenderer.numCornerVertices = 3;
        lineRenderer.numCapVertices = 1;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.positionCount = timeValue + 1;*/
        //line.transform.position = new Vector3(origo.x, origo.y, 0);
        //lineRenderer.SetPosition(0, new Vector3(line.transform.position.x, line.transform.position.y, 0));
        //triangles[0] = 0;
        /*for (int i = 0; i < timeValue+1; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(origo.x + i * (graphBoard.transform.localScale.x / timeValue), origo.y + (values[i] * (graphBoard.transform.localScale.y / maxValue)) * 0.75f, 0));
        }*/
       
        //vertices[timeValue+1] = new Vector3(graphBoard.transform.position.x + (graphBoard.transform.localScale.x / 2), graphBoard.transform.position.y + (graphBoard.transform.localScale.y / 2), 0);
        //triangles[timeValue + 1] = timeValue + 1;
        //vertices[timeValue+2] = new Vector3(graphBoard.transform.position.x - (graphBoard.transform.localScale.x / 2), graphBoard.transform.position.y - (graphBoard.transform.localScale.y / 2), 0);
        //triangles[timeValue + 2] = timeValue + 2;
        //lineRenderer.SetPositions(new Vector3[3] { startPos, startPos + new Vector3(1,1,0), startPos + new Vector3(2,0,0) }) ;
        //uv = new Vector2[] {new Vector2(vertices[0].x,vertices[0].y), new Vector2(vertices[1].x, vertices[1].y), new Vector2(vertices[2].x, vertices[2].y) };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizeValues()
    {
        for (int i = 0; i < timeValue + 1; i++)
        {
            values[i] = Random.Range(2, maxValue + 1);
        }
        for (int i = 0; i < timeValue + 1; i++)
        {
            int remainingValues = values[i];
            for (int j = 0; j < geneAmount; j++)
            {
                if (j == geneAmount - 1)
                {
                    genesArr[j, i] = remainingValues;
                }
                else
                {
                    genesArr[j, i] = Random.Range(0, remainingValues + 1);
                    remainingValues -= genesArr[j, i];
                }
            }
        }
    }

    void CreateMeshes()
    {
        for (int i = 0; i < geneAmount; i++)
        {
            vertices = new Vector3[(timeValue + 1) * 2];
            triangles = new int[(timeValue + 1) * 6];
            Mesh mesh = new Mesh();
            GameObject newMesh = new GameObject();
            newMesh.AddComponent<MeshRenderer>();
            newMesh.AddComponent<MeshFilter>();
            newMesh.name = "" + i;
            newMesh.transform.parent = graphBoard.transform;
            newMesh.GetComponent<MeshFilter>().mesh = mesh;
            newMesh.GetComponent<Renderer>().material = Instantiate(mat);
            if (i > graphColors.Length)
            {
                newMesh.GetComponent<Renderer>().material.color = new Color(Random.Range(0.00f, 1.01f), Random.Range(0.00f, 1.01f), Random.Range(0.00f, 1.01f));
            }
            else
            {
                newMesh.GetComponent<Renderer>().material.color = graphColors[i];
            }
            AssignVertices(i);
            SetTriangles();
            mesh.vertices = vertices;
            mesh.SetUVs(0, vertices);
            mesh.triangles = triangles;
            //triangles[i] = i;  
        }
    }

    void AssignVertices(int index)
    {
        int valuesIndex = 0;
        for (int j = 0; j < (timeValue+1) * 2; j += 2)
        {

            if (index == geneAmount-1)
            {
                vertices[j] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + (values[valuesIndex] * (graphBoard.transform.localScale.y / maxValue) * 0.75f), graphBoard.transform.position.z - 1);
            }
            else
            {
                vertices[j] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + ((values[valuesIndex] * (graphBoard.transform.localScale.y / maxValue) * 0.75f) * (((float)genesArr[index , valuesIndex] + (float)GetOffset(index, valuesIndex)) / (float)values[valuesIndex])), graphBoard.transform.position.z - 1);
            }

            if (index == 0)
            {
                vertices[j + 1] = new Vector3(vertices[j].x, origo.y, graphBoard.transform.position.z - 1);
            }
            else
            {
                vertices[j + 1] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + ((values[valuesIndex] * (graphBoard.transform.localScale.y / maxValue) * 0.75f) * (((float)genesArr[index - 1, valuesIndex] + +(float)GetOffset(index-1, valuesIndex)) / (float)values[valuesIndex])), graphBoard.transform.position.z - 1);
            }

            //Debug.Log("Success!")
            valuesIndex++;
            //triangles[i] = i;
        }
    }

    void SetTriangles()
    {
        int verticeNum = 0;
        for (int i = 0; i < (timeValue) * 6; i += 6)
        {

            triangles[i] = verticeNum;
            triangles[i + 1] = verticeNum + 2;
            triangles[i + 2] = verticeNum + 1;
            triangles[i + 3] = verticeNum + 1;
            triangles[i + 4] = verticeNum + 2;
            triangles[i + 5] = verticeNum + 3;
            verticeNum += 2;
            //Debug.Log("Success!" + i);
        }
    }

    float GetOffset(int index, int valuesIndex)
    {
        float offset = 0;
        for(int i = 0; i < index; i++)
        {
            offset += (float)genesArr[i, valuesIndex];
        }
        return offset;
    }

    void SetColors()
    {
        graphColors = new Color[15] { new Color(0.7f, 0.3f, 1.0f), new Color(1.0f, 0.4f, 0.4f), new Color(0.4f, 1.0f, 0.4f), 
                                    new Color(1.0f, 1.0f, 0.4f), new Color(1.0f, 0.6f, 0.3f), new Color(0.4f, 0.4f, 1.0f), 
                                    new Color(0.3f, 0.8f, 0.8f), new Color(0.85f, 0.4f, 0.7f), new Color(0.5f, 0.35f, 0.25f), 
                                    new Color(0.6f, 0.25f, 0.25f), new Color(0.3f, 0.5f, 0.25f), new Color(0.2f, 0.2f, 0.4f), 
                                    new Color(0.3f, 0.2f, 0.4f), new Color(0.4f, 0.2f, 0.35f), new Color(0.25f, 0.4f, 0.4f) };
    }
}
