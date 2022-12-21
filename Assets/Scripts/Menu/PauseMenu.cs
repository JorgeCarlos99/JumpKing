using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = true;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject pauseMenuUISpears;
    public GameObject kaladinSpear;
    public GameObject teftSpear;
    public AudioMixer audioMixer;
    public static PauseMenu instance;
    public Slider sliderEffect;
    public Slider sliderMusic;
    public GameObject chargedJumpSlider;

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

        if (OptionsMenuPause.instance.ezMode)
        {
            chargedJumpSlider.SetActive(true);
        }
        else
        {
            chargedJumpSlider.SetActive(false);
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

            Time.timeScale = 0f;
            GameIsPaused = true;
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
        // Save Volume
        float volumeMusicValue;
        bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
        float volumeEffectValue;
        bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);
        SaveManager.instance.activeSave.musicVolume = volumeMusicValue;
        SaveManager.instance.activeSave.effectVolume = volumeEffectValue;

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

        // Save Volume
        float volumeMusicValue;
        bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
        float volumeEffectValue;
        bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);
        SaveManager.instance.activeSave.musicVolume = volumeMusicValue;
        SaveManager.instance.activeSave.effectVolume = volumeEffectValue;

        SaveManager.instance.Save();
        Debug.Log("Enhorabuena elden ring, saliste del videojuego 2");

        Application.Quit();
    }
}
