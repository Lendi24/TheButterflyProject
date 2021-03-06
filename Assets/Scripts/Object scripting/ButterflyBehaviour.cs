using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public GameObject gameBoard;
    public AudioClip audioClip;

    public Gene gene;

    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            gameBoard.GetComponent<GameManager>().ButterClick(gameObject, audioClip);
            

            //GameObject.Find("GameBoard").GetComponent<GameManager>().ButterClick(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
