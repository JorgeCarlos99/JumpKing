using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScriptMove : MonoBehaviour
{
    public GameObject Point;
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject pauseMenuUISpears;
    public GameObject kaladinSpear;
    public GameObject teftSpear;
    public Slider sliderEffect;
    public Slider sliderMusic;
    public AudioMixer audioMixer;
    [SerializeField] private AudioSource moveMainMenuCursor;
    public int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public static PauseMenuScriptMove instance;

    private void Awake()
    {
        instance = this;
    }

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
        if (optionsMenuUI.activeSelf)
        {
            Debug.Log("Menu opciones activado");
            pauseMenuUI.SetActive(true);
            pauseMenuUISpears.SetActive(true);

            kaladinSpear.SetActive(true);
            teftSpear.SetActive(true);
        }
        else
        {
            Debug.Log("Menu opciones no activado");
            pauseMenuUI.SetActive(false);
            pauseMenuUISpears.SetActive(false);

            kaladinSpear.SetActive(false);
            teftSpear.SetActive(false);

            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuUISpears.SetActive(true);

        // Kaladin Spear check
        if (GameObject.Find("SpearKaladinV1"))
        {
            kaladinSpear.SetActive(false);
        }
        else
        {
            kaladinSpear.SetActive(true);
        }
        // Teft spear check
        if (GameObject.Find("SpearKaladinV2"))
        {
            teftSpear.SetActive(false);
        }
        else
        {
            teftSpear.SetActive(true);
        }
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            // Resume
            ResumePauseMenu();
        }
        else if (SelectedButton == 2)
        {
            // SAVE AND MENU
            SaveAndMenuFromPauseMenu();
        }
        else if (SelectedButton == 3)
        {
            // Options
            OptionsMenuFromPauseMenu();

        }
        else if (SelectedButton == 4)
        {
            // Quit and save
            QuitAndSaveFromPauseMenu();
        }

    }

    public void QuitAndSaveFromPauseMenu()
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
        // Save Volume
        float volumeMusicValue;
        bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
        float volumeEffectValue;
        bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);
        SaveManager.instance.activeSave.musicVolume = volumeMusicValue;
        SaveManager.instance.activeSave.effectVolume = volumeEffectValue;

        SaveManager.instance.Save();
        Debug.Log("saliste del videojuego pausa menu tal");
        PauseMenu.instance.QuitGame();
    }

    public void OptionsMenuFromPauseMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        GameIsPaused = true;
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
    }

    public static void SaveAndMenuFromPauseMenu()
    {
        PauseMenu.instance.LoadMenu();
    }

    public static void ResumePauseMenu()
    {
        PauseMenu.instance.Resume();
    }

    private void OnButtonUp()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (SelectedButton > 1)
        {
            SelectedButton -= 1;
            moveMainMenuCursor.Play();
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
            moveMainMenuCursor.Play();
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