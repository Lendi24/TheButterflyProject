using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphBoardScript : MonoBehaviour
{
    public GameObject graphBoard; //GraphBoardScript
    public GameObject graphPoint;
    public UIDocument graphUI; //GraphBoardScript
    public UIDocument infoUI;
    public StyleSheet noGraphSwitch; //GraphBoardScript
    public int timeValue; //GraphBoardScript
    public int maxValue; //GraphBoardScript
    int colorAmount; //GraphBoardScript
    int domMode; //GraphBoardScript
    int graphNum = 1; //GraphBoardScript
    int screenSizeX = Screen.width; //GraphBoardScript
    int screenSizeY = Screen.height; //GraphBoardScript
    int score;
    List<StatSave> values; //GraphBoardScript
    int[,] colorValueArr; //GraphBoardScript
    Color[] alleleColors; //GraphBoardScript
    Color[] phenotypeColors; //GraphBoardScript

    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(1);
        //GraphBoardScript
        graphBoard.transform.localScale = GameBoardResizer.GetGameBoardSize() * 10;
        GetVariables(0);
        //colorValueArr = new int[colorAmount, timeValue + 1];
        //int[] triangles = new int[timeValue+3];
        graphUI.GetComponent<GraphUI>().SetScore(score);
        graphBoard.GetComponent<GraphScript>().SetColors();
        alleleColors = graphBoard.GetComponent<GraphScript>().CreateColors(3);
        while (true)
        {
            phenotypeColors = graphBoard.GetComponent<GraphScript>().CreateColors(2);
            if (phenotypeColors[0] != alleleColors[0])
            {
                break;
            }
        }
        //RandomizeValues();

        graphBoard.GetComponent<GraphScript>().CreateMeshes(0,colorAmount,timeValue,maxValue,colorValueArr,graphBoard,alleleColors,phenotypeColors,values);
        graphPoint.GetComponent<GraphPointScript>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //GraphBoardScript
        if (screenSizeX != Screen.width || screenSizeY != Screen.height)
        {
            screenSizeX = Screen.width;
            screenSizeY = Screen.height;
            graphBoard.transform.localScale = GameBoardResizer.GetGameBoardSize() * 10;
        }
    }

    //GraphBoardScript
    public void GetVariables(int colorValueMode)
    {
        timeValue = SoundScript.timeValue;
        //Debug.Log(timeValue);

        //Debug.Log(colorAmount);
        values = SoundScript.values;
        domMode = values[0].domMode;
        score = SoundScript.score;

        //Gene[] test = values[0].geneData;
        //Debug.Log(GeneticManager.BlendInCalc(test[0]));
        if (domMode != 0)
        {
            graphUI.GetComponent<GraphUI>().ChangeText("Allele Graph");
            if (colorValueMode == 0) //Gets the color values for alleles
            {
                colorAmount = SoundScript.colorValue;
                //Debug.Log(values.Count);
                colorValueArr = new int[colorAmount, timeValue + 1];
                for (int i = 0; i < values.Count; i++)
                {
                    bool[][] arrData = values[i].alleleData;
                    for (int j = 0; j < arrData.Length; j++)
                    {
                        int gene = 0;
                        for (int k = 0; k < arrData[j].Length; k++)
                        {
                            if (arrData[j][k])
                            {
                                gene++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        colorValueArr[gene, i]++;
                    }
                }
            }
            else if (colorValueMode == 1) //Gets the color values for phenotypes
            {
                colorAmount = 2;
                colorValueArr = new int[2, timeValue + 1];
                for (int i = 0; i < values.Count; i++)
                {
                    for (int j = 0; j < values[i].phenotypeData.Length; j++)
                    {
                        //Debug.Log(GeneticManager.BlendInCalc(values[i].phenotypeData[j]));
                        if (values[i].phenotypeData[j] == 1)
                        {
                            colorValueArr[0, i]++;
                        }
                        else if (values[i].phenotypeData[j] == 0.5f)
                        {
                            colorValueArr[1, i]++;
                        }

                    }
                }
            }
        }
        else
        {
            graphUI.GetComponent<GraphUI>().ChangeText("Allele/Phenotype Graph");
            graphUI.rootVisualElement.styleSheets.Add(noGraphSwitch);
            colorAmount = SoundScript.colorValue;
            //Debug.Log(values.Count);
            colorValueArr = new int[colorAmount, timeValue + 1];
            for (int i = 0; i < values.Count; i++)
            {
                bool[][] arrData = values[i].alleleData;
                for (int j = 0; j < arrData.Length; j++)
                {
                    int gene = 0;
                    for (int k = 0; k < arrData[j].Length; k++)
                    {
                        if (arrData[j][k])
                        {
                            gene++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    colorValueArr[gene, i]++;
                }
            }
        }
        for (int i = 0; i < values.Count; i++)
        {
            if (maxValue < values[i].populationAmount)
            {
                maxValue = values[i].populationAmount;
            }
        }
    }

    //GraphBoardScript
    public void SwitchGraph()
    {
        graphBoard.GetComponent<GraphScript>().ResetGraph();
        if (graphNum == 1)
        {
            graphNum = 2;
            GetVariables(1);
            graphBoard.GetComponent<GraphScript>().CreateMeshes(1, colorAmount, timeValue, maxValue, colorValueArr, graphBoard, alleleColors, phenotypeColors, values);
            graphUI.GetComponent<GraphUI>().ChangeText("Phenotype Graph");
        }
        else if (graphNum == 2)
        {
            graphNum = 1;
            GetVariables(0);
            graphBoard.GetComponent<GraphScript>().CreateMeshes(0, colorAmount, timeValue, maxValue, colorValueArr, graphBoard, alleleColors, phenotypeColors, values);
            graphUI.GetComponent<GraphUI>().ChangeText("Allele Graph");
        }
        infoUI.GetComponent<InfoUI>().UpdatePosition();
        graphPoint.GetComponent<GraphPointScript>().GetRoundInfo();
        infoUI.GetComponent<InfoUI>().DestroyStats();
        AssignStats();
    }

    public void AssignStats()
    {
        string[][] stats = new string[colorAmount][];
        for(int i = 0; i < stats.Length; i++)
        {
            stats[i] = new string[] {GetStatName(graphNum,i),"" + colorValueArr[i,graphPoint.GetComponent<GraphPointScript>().round]};
        }
        Color[] colors = new Color[1];
        if(graphNum == 1)
        {
            colors = alleleColors;
        }
        else if(graphNum == 2)
        {
            colors = phenotypeColors;
        }
        infoUI.GetComponent<InfoUI>().CreateGraphStats(colorAmount, stats, graphPoint.GetComponent<GraphPointScript>().round, colors);
    }

    public string GetStatName(int mode, int index)
    {
        string name = "";

        if(mode == 1)
        {
            switch(index)
            {
                case 0:
                    name = "aa";
                    break;
                case 1:
                    name = "Aa";
                    break;
                case 2:
                    name = "AA";
                    break;
            }
        }
        else if(mode == 2)
        {
            switch (index)
            {
                case 0:
                    name = "a";
                    break;
                case 1:
                    name = "A";
                    break;
            }
        }

        return name;
    }
}
