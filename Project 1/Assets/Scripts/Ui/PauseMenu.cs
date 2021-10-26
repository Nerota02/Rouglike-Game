using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public bool optionsOpen = false;
    public bool controlsOpen = false;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject InventoryPanel;
    public GameObject controlsMenu;
    public GameObject Player;
    public GameObject[] Enemies;
    public Button optionsButton;

    void Start()
    {

    }

    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
                //Cursor.visible = false;
            }
            else
            {
                PauseGame();

                //Cursor.visible = true;
            }
        }


    }

    public void PauseGame()
    {
        if (Player.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            FindObjectOfType<AudioManager>().Play("UI Confirmation");
            optionsMenu.SetActive(false);
            //controlsMenu.SetActive(false);

            Time.timeScale = 0f;
            gamePaused = true;
            optionsOpen = false;
            Player.SetActive(false);

            //Enemies[0].SetActive(false);
            //Enemies[1].SetActive(false);

        }
        else
        {
            pauseMenu.SetActive(false);
        }

    }
    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        AudioListener.pause = false;
        optionsOpen = false;
        Player.SetActive(true);

        //Enemies[0].SetActive(true);
        //Enemies[1].SetActive(true);

    }

    public void Controls()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Inventory()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        InventoryPanel.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        SceneManager.LoadScene("Main_Menu");
    }

    public void OpenOptionsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        optionsOpen = true;
        gamePaused = false;
    }
    public void CloseOptionsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        optionsOpen = false;
        gamePaused = true;
    }
    public void CloseControlsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        controlsOpen = false;
        gamePaused = true;
    }

    public void OnMouseOver()
    {
        AudioListener.pause = false;
        FindObjectOfType<AudioManager>().OnMouseOver("Hover");
    }
}
