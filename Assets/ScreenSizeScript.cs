using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameBoardResizer.ChangedScreenSize(gameObject.transform.localScale.x *10f))
        {
            Debug.Log("true");
            GetComponent<GameManager>().SetScreenSize();
        }
    }
}
