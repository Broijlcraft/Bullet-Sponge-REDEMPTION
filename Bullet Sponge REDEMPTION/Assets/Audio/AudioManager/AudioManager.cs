using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region singleton
    public static AudioManager single;
    
    private void Awake()
    {
        if (!single)
        {
            single = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public AudioMixer audioMixer;
    public AudioMixerGroup sfx;
    public AudioMixerGroup music;
    public AudioMixerGroup master;

}
