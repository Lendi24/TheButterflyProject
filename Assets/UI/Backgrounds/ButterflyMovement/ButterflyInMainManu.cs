using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInMainManu : MonoBehaviour
{
    [SerializeField]
    GameObject butterContainer, butterfly;

    [SerializeField]
    Camera cam;

    static float size;
    float screenWidth;
    float rot;
    Vector3 topLeft;
    Vector3 bottomRight;
    static GameObject staticButterfly;
    static GameObject staticButterContainer;
    // Start is called before the first frame update
    void Start()
    {
        staticButterfly = butterfly;
        staticButterContainer = butterContainer;
        LoadBackground();
    }

    private void Update()
    {
        if(screenWidth != Screen.width)
        {
            Debug.Log("ee");
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            } 
            transform.rotation = new Quaternion(0, 0, 0, 0);
            LoadBackground();
        }
    }

    void LoadBackground()
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
        screenWidth = Screen.width;
        rot = Mathf.Acos((bottomRight.x - topLeft.x) / size) * Mathf.Rad2Deg;

        butterContainer.transform.localScale = new Vector3(Mathf.Ceil(size), Mathf.Ceil(size)+1, butterContainer.transform.localScale.z);
    }

    void SpawnMarkers()
    {
        for(int i = 0; i < Mathf.CeilToInt(size)/3+1; i++)
        {
            GameObject startMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            startMarker.transform.parent = butterContainer.transform;
            startMarker.transform.name = "Start" + i;
            startMarker.transform.position = new Vector3(i*3 - (size/2)+1, butterContainer.transform.position.y - ((size + 0.5f) / 2),0);
            startMarker.GetComponent<MeshRenderer>().enabled = false;

            GameObject endMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            endMarker.transform.parent = startMarker.transform;
            endMarker.transform.name = "End" + i;
            endMarker.transform.position = new Vector3(i * 3 - (size / 2) + 1, startMarker.transform.position.y + (size + 0.8f), 0);
            endMarker.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void SpawnButterflies()
    {
        float buttersPerColumn = Mathf.Floor((size + 1) / 1.5f) - 1;
        float remainingSpace = size - buttersPerColumn;
        float distance = 1 + (remainingSpace / buttersPerColumn);

        for (int i = 0; i < Mathf.CeilToInt(size)/3+1; i++)
        {
            for(int j = 0; j < buttersPerColumn; j++)
            {
                CreateButterfly(i * 3 - (size / 2) + 1, j * distance - (size / 2), -0.5f);
            }
        }
    }

    public static void CreateButterfly(float x, float y, float z)
    {
        GameObject newButterfly = Instantiate(staticButterfly);
        newButterfly.transform.parent = staticButterContainer.transform;
        newButterfly.transform.position = new Vector3(x,y,z);
        newButterfly.transform.localScale = new Vector3(newButterfly.transform.localScale.x, newButterfly.transform.localScale.x, newButterfly.transform.localScale.x);
        newButterfly.transform.rotation = new Quaternion(0, 0, 0, 0);
        newButterfly.transform.name = "Butterfly";
        newButterfly.AddComponent<MenuButterflyMovement>();
        newButterfly.AddComponent<BoxCollider>();
        newButterfly.AddComponent<Rigidbody>();
        Rigidbody butterRigid = newButterfly.GetComponent<Rigidbody>();
        butterRigid.constraints = RigidbodyConstraints.FreezeRotation;
        butterRigid.useGravity = false;
    }

    public static float GetContPos()
    {

        return size;
    }
}
