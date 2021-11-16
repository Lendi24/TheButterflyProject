using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawning : MonoBehaviour
{
    static float size;
    static bool spawnButterflies;
    // Start is called before the first frame update
    void Start()
    {
        spawnButterflies = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnButterflies && size != 0)
        {
            if (TimmerManagment.Timmer(0.5f))
            {
                for (int i = 0; i < Mathf.CeilToInt(size / 3); i++)
                {
                    GameObject startMarker = transform.GetChild(i).gameObject;
                    ButterflyInMainManu.CreateButterfly(startMarker.transform.position.x, startMarker.transform.position.y, startMarker.transform.position.z - 0.5f);
                }
            }
        }
    }

    public static void SetSize(float _size)
    {
        size = _size;
        Debug.Log(size);
    }

    public static void EnableSpawning(bool enabled)
    {
        spawnButterflies = enabled;
    }
}
