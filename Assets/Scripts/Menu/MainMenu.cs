using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider sliderEffect;
    public Slider sliderMusic;
    public AudioMixer audioMixer;
    public GameObject Point;
    public GameObject options;
    public GameObject menu;
    public GameObject sureMenu;
    public GameObject creditsMenu;
    [SerializeField] private AudioSource moveMainMenuCursor;
    [SerializeField] private AudioSource spaceMainMenuCursor;
    public int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public Transform ButtonPosition5;
    // public void Play()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

    // public void RestartGame()
    // {
    //     SaveManager.instance.DeleteSavedData();
    // }

    // public void Quit()
    // {
    //     Debug.Log("Enhorabuena elden ring, saliste del videojuego main menu");
    //     Application.Quit();
    // }

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            spaceMainMenuCursor.Play();
            // Play
            AudioSource audioMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
            while (audioMusic.volume > 0.01f)
            {
                audioMusic.volume -= Time.deltaTime / 5f;
            }

            string dataPath = Application.persistentDataPath;
            if (System.IO.File.Exists(dataPath + "/" + "Save1.save"))
            {
                Debug.Log("Tiene datos");
                SceneManager.LoadScene("CutScene2");
            }
            else
            {
                Debug.Log("No tiene datos, Primera vez jugando");
                SceneManager.LoadScene("CutScene1");
            }
        }
        else if (SelectedButton == 2)
        {
            // Reset Game
            sureMenu.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
        }
        else if (SelectedButton == 3)
        {
            // Options
            // Sound
            float volumeMusicValue;
            bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
            float valueLog10Music = volumeMusicValue / 20;

            if (resultMusic)
            {
                sliderMusic.SetValueWithoutNotify(Mathf.Pow(10, valueLog10Music));
            }

            float volumeEffectValue;
            bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);
            float valueLog10Effect = volumeEffectValue / 20;

            if (resultEffect)
            {
                sliderEffect.SetValueWithoutNotify(Mathf.Pow(10, valueLog10Effect));
            }
            // end sounds


            options.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
        }
        else if (SelectedButton == 4)
        {
            // Credits
            creditsMenu.SetActive(true);
            menu.SetActive(false);
        }
        else if (SelectedButton == 5)
        {
            // Quit and save
            Debug.Log("saliste del videojuego pausa menu tal");
            Application.Quit();
        }
    }
    private void OnButtonUp()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (SelectedButton > 1)
        {
            moveMainMenuCursor.Play();
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
            moveMainMenuCursor.Play();
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
        else if (SelectedButton == 5)
        {
            Point.transform.position = ButtonPosition5.position;
        }
    }
}
