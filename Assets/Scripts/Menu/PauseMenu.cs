using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    // public GameObject pauseMenuUI;
    // public GameObject pauseFirstButton, menuFisrtButton;

    public static PauseMenu instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        SaveManager.instance.activeSave.position = PlayerControllerNoPhysics.instance.position;
        SaveManager.instance.activeSave.spears = SpearCounter.instance.spears;
        if (!GameObject.Find("SpearKaladinV1"))
        {
            Debug.Log("Saved 1 Spear");
            SaveManager.instance.activeSave.lanza1 = "SpearKaladinV1";
        }
        if (!GameObject.Find("SpearKaladinV2"))
        {
            Debug.Log("Saved 2 Spear");
            SaveManager.instance.activeSave.lanza2 = "SpearKaladinV2";
        }

        SaveManager.instance.Save();
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        SaveManager.instance.activeSave.position = PlayerControllerNoPhysics.instance.position;
        SaveManager.instance.activeSave.spears = SpearCounter.instance.spears;
        if (!GameObject.Find("SpearKaladinV1"))
        {
            Debug.Log("Saved 1 Spear");
            SaveManager.instance.activeSave.lanza1 = "SpearKaladinV1";
        }
        if (!GameObject.Find("SpearKaladinV2"))
        {
            Debug.Log("Saved 2 Spear");
            SaveManager.instance.activeSave.lanza2 = "SpearKaladinV2";
        }

        SaveManager.instance.Save();
        Debug.Log("Enhorabuena elden ring, saliste del videojuego 2");

        Application.Quit();
    }
}
