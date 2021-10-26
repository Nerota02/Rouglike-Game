using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{

    [SerializeField]
    AudioManager audioManager;
    public AudioMixer mixer;

    //bools
    public GameObject Player;
    public bool AppIsQuitting { get; private set; }
    public bool IsPlaying { get; set; }
    public static bool gamePaused;
    private PlayerMovement PCplayer;

    //menu states
    public enum MenuStates { MainMenu, OptionsMenu };

    public MenuStates currentState;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioManager = AudioManager.instance;

        if ((SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Metrics")) || (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MH_Main_Island")))
        {
            //PCplayer = (Player_State_Machine)Player.GetComponent("Player_State_Machine");
        }

        if (audioManager == null)
        {
            Debug.Log("No Audio Manager");
        }

        currentState = MenuStates.MainMenu;
    }

    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            PauseGame();
            
        }
    }

    public void Play()
    {
        Debug.Log("Loading Metrics");
        FindObjectOfType<AudioManager>().Play("Buttons");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
    }

    public void Retry()
    {
        Debug.Log("Retrying game");

        FindObjectOfType<AudioManager>().Play("UI Confirmation");
        Time.timeScale = 1;
        if (Player != null)
        {
            //Entity_HP_System entity_hp_system = Player.GetComponent<Entity_HP_System>();
            //entity_hp_system.IsGameStopped = false;
            //entity_hp_system.ResetPlayer();
        }
        SceneManager.LoadScene("Metrics");
        if (Player == null)
        {
            GameObject PlayerNew = GameObject.FindGameObjectWithTag("Player");
            Player = PlayerNew;
            PlayerNew = null;
        }
    }



    public void Resume()
    {
        Time.timeScale = 1;

    }

    public void WaitResume()
    {
        StartCoroutine(WaitToResume());
    }

    public IEnumerator WaitToResume()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;

    }

    public void Credits()
    {
        Debug.Log("Loading Credits");

        FindObjectOfType<AudioManager>().Play("Buttons");

        SceneManager.LoadScene("Credits");
    }

    public void CreditsBack()
    {
        Debug.Log("Back to the Main Menu");
        FindObjectOfType<AudioManager>().Play("Buttons");

        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {

        Debug.Log("Game Quit");
        FindObjectOfType<AudioManager>().Play("Buttons");
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        AppIsQuitting = true;
    }

    public void MainLoad()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene("Main_Menu");
        FindObjectOfType<AudioManager>().Play("Buttons");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        IsPlaying = false;
        AppIsQuitting = false;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnMouseOver()
    {
        FindObjectOfType<AudioManager>().OnMouseOver("Hover");
    }
}
