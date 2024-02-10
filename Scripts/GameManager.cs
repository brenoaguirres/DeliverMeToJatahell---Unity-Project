using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages game logic

public class GameManager : MonoBehaviour
{
    #region Fields and Properties
    PlayerStatus playerStatus;
    GUIController guiController;
    string PlayerTag = "Player";
    string CanvasName = "Canvas";
    private string mainMenuTag = "MainMenu";
    private bool pausedGame = false;
    private float fixedDeltaTime;
    public GameObject pauseMenu;
    #endregion

    #region Initialization  
    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        if (SceneManager.GetActiveScene().name != mainMenuTag)
        {
            playerStatus = GameObject.FindWithTag(PlayerTag).GetComponent<PlayerStatus>();
            guiController = GameObject.FindWithTag("GUIController").GetComponent<GUIController>();
            Time.timeScale = 1;
        }
    }
    #endregion

    #region Execution
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != mainMenuTag)
        {
            CheckForPlayer();
        }
    }
    #endregion

    #region Methods
    public static void CallScene(string scene)
    {
        switch (scene)
        {
            case "2-1":
                SceneManager.LoadScene("Demo_POLIND1F");
                break;
            case "2-2":
                SceneManager.LoadScene("Demo_POLOUTBCK1F");
                break;
            case "2-3":
                SceneManager.LoadScene("Demo_POLOUTFRT1F");
                break;
            case "2-4":
                SceneManager.LoadScene("Demo_POLSHELTER");
                break;
            case "2-5":
                SceneManager.LoadScene("Test_Arena");
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    public void CheckForPlayer()
    {
        if (!playerStatus.isAlive)
        {
            Time.timeScale = 0;
            guiController.GameOver();
        }
    }

    public static void QuitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void PauseGame()
    {
        pausedGame = !pausedGame;
        if (!pausedGame)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }
    #endregion
}
