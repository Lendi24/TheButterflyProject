using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphScript : MonoBehaviour
{
    public Material mat;
    //LineRenderer lineRenderer;
    //Vector2[] uv;
    Color[] graphColors;
    Vector3[] vertices;
    Vector3 origo;
    int[] triangles;
    public float[] timeValueRangeX;
    // Start is called before the first frame update
    void Start()
    {
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

    /*void RandomizeValues()
    {
        for (int i = 0; i < timeValue + 1; i++)
        {
            values[i] = Random.Range(2, maxValue + 1);
        }
        for (int i = 0; i < timeValue + 1; i++)
        {
            StatSave remainingValues = values[i];
            for (int j = 0; j < colorAmount; j++)
            {
                if (j == colorAmount - 1)
                {
                    colorValueArr[j, i] = remainingValues;
                }
                else
                {
                    colorValueArr[j, i] = Random.Range(0, remainingValues + 1);
                    remainingValues -= colorValueArr[j, i];
                }
            }
        }
    }*/

    //GraphScript
    public void CreateMeshes(int colorValueMode, int colorAmount, int timeValue, int maxValue, int[,] colorValueArr, GameObject graphBoard, Color[] alleleColors, Color[] phenotypeColors, List<StatSave> values)
    {
        for (int i = 0; i < colorAmount; i++)
        {
            timeValueRangeX = new float[timeValue + 1];
            vertices = new Vector3[(timeValue + 1) * 2];
            triangles = new int[(timeValue + 1) * 6];
            origo = new Vector3(graphBoard.transform.position.x - (graphBoard.transform.localScale.x / 2), graphBoard.transform.position.y - (graphBoard.transform.localScale.y / 2), 0);
            Mesh mesh = new Mesh();
            GameObject newMesh = new GameObject();
            newMesh.AddComponent<MeshRenderer>();
            newMesh.AddComponent<MeshFilter>();
            newMesh.name = "" + i;
            newMesh.transform.parent = graphBoard.transform;
            newMesh.GetComponent<MeshFilter>().mesh = mesh;
            newMesh.GetComponent<Renderer>().material = Instantiate(mat);
            newMesh.GetComponent<Renderer>().material.color = AssignColors(i, colorValueMode, alleleColors, phenotypeColors);
            AssignVertices(i, timeValue, colorAmount, graphBoard, values, maxValue, colorValueArr);
            SetTriangles(triangles, timeValue);
            mesh.vertices = vertices;
            mesh.SetUVs(0, vertices);
            mesh.triangles = triangles;
            //triangles[i] = i;  
        }
    }

    //GraphScript
    public Color[] CreateColors(int arrLength)
    {
        int rand = Random.Range(0, 15);
        Color[] newColors = new Color[arrLength];
        for(int i = 0; i < arrLength; i++)
        {
            newColors[i] = new Color(graphColors[rand].r - ((float)i / 10), graphColors[rand].g - ((float)i / 10), graphColors[rand].b - ((float)i / 10));
        }

        return newColors;
    }

    //GraphScript
    Color AssignColors(int meshNr,int colorValueMode, Color[] alleleColors, Color[] phenotypeColors)
    {
        if(colorValueMode == 0)
        {
            return alleleColors[meshNr];
        }
        else if(colorValueMode == 1)
        {
            return phenotypeColors[meshNr];
            /*if (meshNr > graphColors.Length)
            {
                return new Color(Random.Range(0.00f, 1.01f), Random.Range(0.00f, 1.01f), Random.Range(0.00f, 1.01f));
            }
            else
            {
                return graphColors[meshNr];
            }*/
        }
        return new Color(0, 0, 0);
    }

    //GraphScript
    void AssignVertices(int index, int timeValue, int colorAmount, GameObject graphBoard, List<StatSave> values, int maxValue, int[,] colorValueArr)
    {
        int valuesIndex = 0;
        for (int j = 0; j < (timeValue+1) * 2; j += 2)
        {

            if (index == colorAmount-1)
            {
                vertices[j] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + (values[valuesIndex].populationAmount * (graphBoard.transform.localScale.y / maxValue) * 0.75f), graphBoard.transform.position.z - 1);
            }
            else
            {
                vertices[j] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + ((values[valuesIndex].populationAmount * (graphBoard.transform.localScale.y / maxValue) * 0.75f) * (((float)colorValueArr[index , valuesIndex] + (float)GetOffset(index, valuesIndex,colorValueArr)) / (float)values[valuesIndex].populationAmount)), graphBoard.transform.position.z - 1);
            }

            if (index == 0)
            {
                vertices[j + 1] = new Vector3(vertices[j].x, origo.y, graphBoard.transform.position.z - 1);
            }
            else
            {
                vertices[j + 1] = new Vector3(origo.x + (valuesIndex * (graphBoard.transform.localScale.x / timeValue)), origo.y + ((values[valuesIndex].populationAmount * (graphBoard.transform.localScale.y / maxValue) * 0.75f) * (((float)colorValueArr[index - 1, valuesIndex] + +(float)GetOffset(index-1, valuesIndex,colorValueArr)) / (float)values[valuesIndex].populationAmount)), graphBoard.transform.position.z - 1);
            }

            //Debug.Log("Success!")
            valuesIndex++;
            timeValueRangeX[j / 2] = vertices[j].x;
            //triangles[i] = i;
        }
    }

    //GraphScript
    void SetTriangles(int[] triangles, int timeValue)
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

    //GraphScript
    float GetOffset(int index, int valuesIndex, int[,] colorValueArr)
    {
        float offset = 0;
        for(int i = 0; i < index; i++)
        {
            offset += (float)colorValueArr[i, valuesIndex];
        }
        return offset;
    }

    //GraphScript
    public void SetColors()
    {
        graphColors = new Color[15] { new Color(0.7f, 0.3f, 1.0f), new Color(1.0f, 0.4f, 0.4f), new Color(0.4f, 1.0f, 0.4f), 
                                    new Color(1.0f, 1.0f, 0.4f), new Color(1.0f, 0.6f, 0.3f), new Color(0.4f, 0.4f, 1.0f), 
                                    new Color(0.3f, 0.8f, 0.8f), new Color(0.85f, 0.4f, 0.7f), new Color(0.5f, 0.35f, 0.25f), 
                                    new Color(0.6f, 0.25f, 0.25f), new Color(0.3f, 0.5f, 0.25f), new Color(0.2f, 0.2f, 0.4f), 
                                    new Color(0.3f, 0.2f, 0.4f), new Color(0.4f, 0.2f, 0.35f), new Color(0.25f, 0.4f, 0.4f) };
    }

    //GraphScript
    public void ResetGraph() 
    {
        for(int i = 0; i < transform.childCount; i++) 
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
