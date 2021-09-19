using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public GameObject gameBoard;

    public bool[] genes;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            gameBoard.GetComponent<GameManager>().ButterClick(gameObject);
            //GameObject.Find("GameBoard").GetComponent<GameManager>().ButterClick(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
