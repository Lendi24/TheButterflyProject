using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButterflyMovement : MonoBehaviour
{
    Renderer m_Renderer;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Vector3.Distance(new Vector3(0, 0, 0), transform.up * 3);
        ButterflyInMainManu.SetSpeed(speed);
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
        for(int i = 0; i < ButterflyInMainManu.GetContSize()/3; i++)
        {
            if(collision.gameObject.transform.name == "End"+i)
            {
                //ButterflyInMainManu.CreateButterfly(collision.gameObject.transform.parent.position.x, collision.gameObject.transform.parent.position.y,0);
                Destroy(this.gameObject);
            }
        }
    }
}
