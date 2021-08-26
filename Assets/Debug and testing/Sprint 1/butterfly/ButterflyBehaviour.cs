using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public bool hasCollided = false;

    [SerializeField]
    Material backgroundPattern;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material = backgroundPattern;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.parent.GetComponent<GameManager>().ButterClick(this.gameObject);
        }
    }

    void OnTriggerEnter()
    {
        transform.parent.GetComponent<GameManager>().RandomMoveButterfly(this.gameObject);
    }
}
