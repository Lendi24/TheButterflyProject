using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphPointScript : MonoBehaviour
{
    public GameObject graphBoard;
    public UIDocument infoUI;
    public int round;
    public int gene;
    public int genePopulation;
    public int population;
    float graphPointRange;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        infoUI.GetComponent<InfoUI>().UpdatePosition();
        GetRoundInfo();
        graphBoard.GetComponent<GraphBoardScript>().AssignStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(mousePos != Camera.main.ScreenToWorldPoint(Input.mousePosition))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            infoUI.GetComponent<InfoUI>().UpdatePosition();
            GetRoundInfo();
            infoUI.GetComponent<InfoUI>().DestroyStats();
            graphBoard.GetComponent<GraphBoardScript>().AssignStats();
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, gameObject.transform.position.z);
        }
    }

    void GetRoundInfo()
    {
        graphPointRange = graphBoard.transform.localScale.x / graphBoard.GetComponent<GraphBoardScript>().timeValue;
        for (int i = 0; i < graphBoard.GetComponent<GraphBoardScript>().timeValue+1; i++)
        {
            if (gameObject.transform.position.x > graphBoard.GetComponent<GraphScript>().timeValueRangeX[i] - (graphPointRange/2) && gameObject.transform.position.x < graphBoard.GetComponent<GraphScript>().timeValueRangeX[i] + (graphPointRange / 2))
            {
                round = i;
            }
        }
    }
}
