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
    static float time;
    float rot;
    static float speed;
    float screenWidth;
    float screenHeight;
    Vector3 topLeft;
    Vector3 bottomRight;
    static GameObject staticButterfly;
    static GameObject staticButterContainer;
    Color[] colours;
    // Start is called before the first frame update
    void Start()
    {
        staticButterfly = butterfly;
        staticButterContainer = butterContainer;
        LoadBackground();
        butterContainer.transform.Rotate(new Vector3(0, 0, 180 - rot), Space.Self);

        if (System.DateTime.Now.Month == 6)//6 - pride
        {
            colours = new Color[] {
                Color.red,
                new Color(r: 1.0f, g: 0.55f, b: 0f),
                Color.yellow,
                Color.green,
                Color.blue,
                new Color(r: 0.46f, g: 0.01f, b: 0.53f),
            };
        }

        else if (System.DateTime.Now.Month == 10 && System.DateTime.Now.Day == 31)//10 / 31
        {
            colours = new Color[] {
                new Color(r: 1.0f, g: 0.55f, b: 0f),
                Color.black,
            };
        }

        else if (System.DateTime.Now.Month == 12)//12 - tomten
        {
            colours = new Color[] {
                Color.red,
                Color.green,
            };
        }

        else//Default
        {
            colours = new Color[] {
                Color.white,
            };
        }
    }

    void Update()
    {

        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            LoadBackground();
            butterContainer.transform.Rotate(new Vector3(0, 0, 180 - rot), Space.Self);
        }

        else
        {   
            if (TimmerManagment.Timmer(0.5f))
            {
                for (int i = 0; i < Mathf.CeilToInt(size / 3); i++)
                {
                    GameObject startMarker = transform.GetChild(i).gameObject;
                    CreateButterfly(startMarker.transform.position.x, startMarker.transform.position.y, startMarker.transform.position.z - 0.5f, colours[i % colours.Length]);
                }
            }
        }
    }

    void LoadBackground()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        SetContainerSize();
        SpawnMarkers();
        SpawnButterflies();
    }

    void SetContainerSize()
    {
        topLeft = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
        bottomRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10));
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        size = Vector3.Distance(topLeft, bottomRight);
        rot = Mathf.Acos((bottomRight.x - topLeft.x) / size) * Mathf.Rad2Deg;

        butterContainer.transform.localScale = new Vector3(Mathf.Ceil(size), Mathf.Ceil(size)+1, butterContainer.transform.localScale.z);
    }

    void SpawnMarkers()
    {
        for(int i = 0; i < Mathf.CeilToInt(size/3); i++)
        {
            GameObject startMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            startMarker.transform.parent = butterContainer.transform;
            startMarker.transform.name = "Start" + i;
            startMarker.transform.position = new Vector3(i*3 - (size/2)+1, butterContainer.transform.position.y - ((size + 0.5f) / 2),0);
            startMarker.GetComponent<MeshRenderer>().enabled = false;

            GameObject endMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            endMarker.transform.parent = startMarker.transform;
            endMarker.transform.name = "End" + i;
            endMarker.transform.position = new Vector3(i * 3 - (size / 2) + 1, startMarker.transform.position.y + size + 0.5f, 0);
            endMarker.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void SpawnButterflies()
    {
        float buttersPerColumn = Mathf.Floor((size+1) / 1.5f);
        float remainingSpace = size - buttersPerColumn;
        float distance = 1 + (remainingSpace / buttersPerColumn);

        for (int i = 0; i < Mathf.CeilToInt(size/3); i++)
        {
            for(int j = 0; j < buttersPerColumn; j++)
            {
                //Debug.Log(butterContainer.transform.GetChild(0).position.y);
                CreateButterfly(i * 3 - (size / 2) + 1, butterContainer.transform.GetChild(0).position.y + (j * distance), -0.5f, Color.white);
            }
        }
    }

    public static void CreateButterfly(float x, float y, float z, Color colour)
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

        newButterfly.GetComponent<MeshRenderer>().material.color = colour;
        Rigidbody butterRigid = newButterfly.GetComponent<Rigidbody>();
        butterRigid.constraints = RigidbodyConstraints.FreezeRotation;
        butterRigid.useGravity = false;
    }

    public static float GetContSize()
    {

        return size;
    }

    public static void SetSpeed(float _speed)
    {
        speed = _speed;
        time = 1.5f / speed;
    }
}
