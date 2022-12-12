using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void RestartGame() {
        SaveManager.instance.DeleteSavedData();
    }
    public void Quit()
    {
        
        Debug.Log("Enhorabuena elden ring, saliste del videojuego main menu");
        Application.Quit();
    }
}
