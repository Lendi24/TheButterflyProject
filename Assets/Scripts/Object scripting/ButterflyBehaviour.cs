using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public GameObject gameBoard;
    public AudioClip audioClip;
    bool dead = false;

    public bool[] genes;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && !dead)
        {
            SoundScript.PlayAudio(audioClip);
            gameBoard.GetComponent<GameManager>().ButterClick(gameObject);
            dead = true;

            //GameObject.Find("GameBoard").GetComponent<GameManager>().ButterClick(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
