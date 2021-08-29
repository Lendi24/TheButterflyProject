using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        //float value = Random.Range(0, 11);
        //value /= 10;
        //specificBackground.SetFloat("_LerpValue", value); add this on gamemaster and stuff
        transform.parent = GameObject.Find("ButterCollection").transform;
    }


    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject.Find("GameBoard").GetComponent<GameManager>().ButterClick(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
