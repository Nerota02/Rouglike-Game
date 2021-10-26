using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0.0001f, 1f)]
    public float volume;
    [Range(0.5f, 3f)]
    public float pitch;
    public AudioMixerGroup audioMixerGroup;
    public bool loop = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.outputAudioMixerGroup = _source.outputAudioMixerGroup;
        source.pitch = pitch;
    }

    public void Play()
    {
        source.pitch = pitch;
        source.Play();
    }

    [HideInInspector]
    public AudioSource source;
}
