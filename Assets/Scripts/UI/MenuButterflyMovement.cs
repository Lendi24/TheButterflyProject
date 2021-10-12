using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButterflyMovement : MonoBehaviour
{
    Renderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1.7f*Time.deltaTime,gameObject.transform.position.y - 3*Time.deltaTime,gameObject.transform.position.z);
        gameObject.GetComponent<Rigidbody>().velocity = transform.up * 3;
    }

    /*private void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (ButterflyInMainManu.GetContPos()/2) - 1, gameObject.transform.position.y + ButterflyInMainManu.GetContPos() - 0.5f, gameObject.transform.position.z);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        for(int i = 0; i < ButterflyInMainManu.GetContPos()/3; i++)
        {
            if(collision.gameObject.transform.name == "End"+i)
            {
                gameObject.transform.position = collision.gameObject.transform.parent.position;
            }
        }
    }
}
