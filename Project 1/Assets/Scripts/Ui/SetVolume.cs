using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class SetVolume : MonoBehaviour
{
    public AudioMixer masterMixer;
    float SFXVolume, BGMVolume;
    public Slider BGMSlider;

    public Slider SFXSlider;

    public Slider MasterSlider;

    void Start()
    {
        BGMSlider.value = UnityEngine.PlayerPrefs.GetFloat("BGMVolume", 0.75f);

        SFXSlider.value = UnityEngine.PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        MasterSlider.value = UnityEngine.PlayerPrefs.GetFloat("MasterVolume", 0.75f);
    }

    public void SetLevelSFX()
    {
        float SFXValue = SFXSlider.value;

        masterMixer.SetFloat("SFXVolume", Mathf.Log10(SFXValue) * 20);
        UnityEngine.PlayerPrefs.SetFloat("SFXVolume", SFXValue);

        Debug.Log("SXF Volume:" + SFXValue);
    }

    public void SetLevelBG()
    {
        float BGMValue = BGMSlider.value;

        masterMixer.SetFloat("BGMVolume", Mathf.Log10(BGMValue) * 20);
        UnityEngine.PlayerPrefs.SetFloat("BGMVolume", BGMValue);

        Debug.Log("BG Volume:" + BGMValue);
    }
    
    public void SetLevelMaster()
    {
        float MasterValue = MasterSlider.value;

        masterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterValue) * 20);
        UnityEngine.PlayerPrefs.SetFloat("MasterVolume", MasterValue);

        Debug.Log("Master Volume:" + MasterValue);
    }

    public void OnMouseDown()
    {

        FindObjectOfType<AudioManager>().Play("Buttons");
    }
}
