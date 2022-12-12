using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseFirstButton, menuFisrtButton;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        // clear selected objects
        EventSystem.current.SetSelectedGameObject(null);

        // set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

        GameIsPaused = true;
    }

    public void LoadMenu()
    {

        Time.timeScale = 1f;
        ;
        SaveManager.instance.activeSave.position = PlayerControllerNoPhysics.instance.position;
        Debug.Log(transform.position);
        SaveManager.instance.Save();
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Enhorabuena elden ring, saliste del videojuego 2");
        Application.Quit();
    }
}
