using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInMainManu : MonoBehaviour
{
    [SerializeField]
    GameObject butterContainer, butterfly;

    [SerializeField]
    Camera cam;

    bool allowSpawning;
    static float size;
    static float time;
    float rot;
    static float speed;
    float screenWidth;
    Vector3 topLeft;
    Vector3 bottomRight;
    static GameObject staticButterfly;
    static GameObject staticButterContainer;
    // Start is called before the first frame update
    void Start()
    {
        allowSpawning = true;
        staticButterfly = butterfly;
        staticButterContainer = butterContainer;
        LoadBackground();
        butterContainer.transform.Rotate(new Vector3(0, 0, 180 - rot), Space.Self);
    }

    void Update()
    {

        if (screenWidth != Screen.width)
        {
            allowSpawning = false;
        }

        if (TimmerManagment.Timmer(0.5f))
        {
            if (allowSpawning)
            {
                for (int i = 0; i < Mathf.CeilToInt(size / 3); i++)
                {
                    GameObject startMarker = transform.GetChild(i).gameObject;
                    CreateButterfly(startMarker.transform.position.x, startMarker.transform.position.y, startMarker.transform.position.z - 0.5f);
                }
            }
            else
            {
                allowSpawning = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                LoadBackground();
                butterContainer.transform.Rotate(new Vector3(0, 0, 180 - rot), Space.Self);
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
        size = Vector3.Distance(topLeft, bottomRight);
        ButterflySpawning.SetSize(size);
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
            endMarker.transform.position = new Vector3(i * 3 - (size / 2) + 1, startMarker.transform.position.y + (size + 0.8f), 0);
            endMarker.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void SpawnButterflies()
    {
        float buttersPerColumn = Mathf.Floor((size + 1) / 1.5f) - 1;
        float remainingSpace = size - buttersPerColumn;
        float distance = 1 + (remainingSpace / buttersPerColumn);

        for (int i = 0; i < Mathf.CeilToInt(size/3); i++)
        {
            for(int j = 0; j < buttersPerColumn; j++)
            {
                CreateButterfly(i * 3 - (size / 2) + 1, butterContainer.transform.GetChild(i).position.y + j * distance, -0.5f);
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
