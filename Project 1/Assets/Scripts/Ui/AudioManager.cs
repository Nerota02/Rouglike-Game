using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
using System.Linq;


public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;
    public static AudioManager instance;
    public AudioMixer masterMixer;

    void Awake()
    {
        //DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }
    public void Play(string name)
    {
        //AudioSource s = soundpairing.Single(sound => sound.Key == name).Value;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        s.source.Play();
        //soundpairing[name].Play();

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Incorrect Sound");
            return;
        }

        s.source.Stop();
    }
    public void OnMouseOver(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Incorrect Sound");
            return;
        }

        s.source.Play();
    }

    public void OnMouseUp(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Incorrect Sound");
            return;
        }

        s.source.Play();
    }
}
