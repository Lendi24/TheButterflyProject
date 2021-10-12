using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInMainManu : MonoBehaviour
{
    public GameObject butterContainer;
    public Camera cam;
    public GameObject butterfly;

    static float size;
    float rot;
    Vector3 topLeft;
    Vector3 bottomRight;
    // Start is called before the first frame update
    void Start()
    {
        SetContainerSize();
        SpawnMarkers();
        SpawnButterflies();
        butterContainer.transform.Rotate(new Vector3(0, 0, 180 - rot), Space.Self);
    }

    void SetContainerSize()
    {
        topLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
        bottomRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10));

        size = Vector3.Distance(topLeft, bottomRight);
        rot = Mathf.Acos((bottomRight.x - topLeft.x) / size) * Mathf.Rad2Deg;

        butterContainer.transform.localScale = new Vector3(Mathf.Ceil(size), Mathf.Ceil(size), butterContainer.transform.localScale.z);
    }

    void SpawnMarkers()
    {
        for(int i = 0; i < Mathf.CeilToInt(size)/3+1; i++)
        {
            GameObject startMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            startMarker.transform.parent = butterContainer.transform;
            startMarker.transform.name = "Start" + i;
            startMarker.transform.position = new Vector3(i*3 - (size/2)+1, butterContainer.transform.position.y*1.5f - (size/2),0);

            GameObject endMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            endMarker.transform.parent = startMarker.transform;
            endMarker.transform.name = "End" + i;
            endMarker.transform.position = new Vector3(i * 3 - (size / 2) + 1, butterContainer.transform.position.y * 1.5f + (size / 2), 0);
        }
    }

    void SpawnButterflies()
    {
        for(int i = 0; i < Mathf.CeilToInt(size)/3+1; i++)
        {
            for(int j = 0; j < Mathf.CeilToInt(size/1.5f)-1; j++)
            {
                GameObject newButterfly = Instantiate(butterfly);
                newButterfly.transform.parent = butterContainer.transform;
                newButterfly.transform.position = new Vector3(i*3 - (size/2)+1, j*1.5f - (size/2), 0);
                newButterfly.transform.localScale = new Vector3(newButterfly.transform.localScale.x, newButterfly.transform.localScale.x, newButterfly.transform.localScale.x);
                newButterfly.transform.Rotate(new Vector3(90,0,0), Space.Self);
                newButterfly.transform.name = "Butterfly";
                newButterfly.AddComponent<MenuButterflyMovement>();
                newButterfly.AddComponent<BoxCollider>();
                newButterfly.AddComponent<Rigidbody>();
                Rigidbody butterRigid = newButterfly.GetComponent<Rigidbody>();
                butterRigid.constraints = RigidbodyConstraints.FreezeRotation;
                butterRigid.useGravity = false;
            }
        }
    }

    public static float GetContPos()
    {

        return size;
    }
}
