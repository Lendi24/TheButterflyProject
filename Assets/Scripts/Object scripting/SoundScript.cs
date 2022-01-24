using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    static SoundScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static SoundScript Instance
    {
        get { return instance; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayAudio(AudioClip audioClip)
    {
        instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        instance.gameObject.GetComponent<AudioSource>().Play();
    }
}
