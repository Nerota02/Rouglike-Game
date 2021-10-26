using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System; 

public class CreditsBack : MonoBehaviour
{
    public AudioMixer Mixer;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void Back()
    {
        //StartCoroutine(Credits());

        Debug.Log("Back to Main Menu");

        FindObjectOfType<AudioManager>().Play("Buttons");

        SceneManager.LoadScene("Main_Menu");

    }

    public void OnMouseOver()
    {
        FindObjectOfType<AudioManager>().Play("Hover");
    }
}
