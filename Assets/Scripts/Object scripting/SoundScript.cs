using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    static SoundScript instance;
    static public int timeValue;
    static public int colorValue;
    static public List<StatSave> values;
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

    public static void PlayAudio(AudioClip audioClip)
    {
        instance.gameObject.GetComponent<AudioSource>().clip = audioClip;
        instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public static void SetVariables(int _timeValue, int _colorValue, List<StatSave> _values)
    {
        timeValue = _timeValue;
        colorValue = _colorValue;
        values = _values;
    }
}
