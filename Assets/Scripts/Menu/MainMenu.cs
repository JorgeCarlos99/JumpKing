using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Point;
    public GameObject options;
    public GameObject menu;
    public int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    // public void Play()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

    public void RestartGame()
    {
        SaveManager.instance.DeleteSavedData();
    }
    // public void Quit()
    // {
    //     Debug.Log("Enhorabuena elden ring, saliste del videojuego main menu");
    //     Application.Quit();
    // }

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            // Play
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SelectedButton == 2)
        {
            // Reset Game
            SaveManager.instance.DeleteSavedData();
        }
        else if (SelectedButton == 3)
        {
            // Options
            options.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
        }
        else if (SelectedButton == 4)
        {
            // Quit and save
            Debug.Log("saliste del videojuego pausa menu tal");
            PauseMenu.instance.QuitGame();
        }
    }
    private void OnButtonUp()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (SelectedButton > 1)
        {
            SelectedButton -= 1;
        }
        MoveThePointer();
        return;
    }
    private void OnButtonDown()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves down one button
        if (SelectedButton < NumberOfButtons)
        {
            SelectedButton += 1;
        }
        MoveThePointer();
        return;
    }
    private void MoveThePointer()
    {
        // Moves the pointer
        if (SelectedButton == 1)
        {
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2)
        {
            Point.transform.position = ButtonPosition2.position;
        }
        else if (SelectedButton == 3)
        {
            Point.transform.position = ButtonPosition3.position;
        }
        else if (SelectedButton == 4)
        {
            Point.transform.position = ButtonPosition4.position;
        }
    }
}
