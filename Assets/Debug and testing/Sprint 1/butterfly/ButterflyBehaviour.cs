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
        Material specificBackground = Instantiate(backgroundPattern);  
        float value = Random.Range(0, 11);
        value /= 10;
        specificBackground.SetFloat("_LerpValue", value);
        this.GetComponent<Renderer>().material = specificBackground;
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
            print("no");
            transform.parent.GetComponent<GameManager>().ButterClick(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("yes");
        hasCollided = true;
    }
}
