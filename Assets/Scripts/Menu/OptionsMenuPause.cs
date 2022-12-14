using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuPause : MonoBehaviour
{
    public GameObject Point;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public AudioMixer audioMixer;
    public Slider sliderEffect;
    public Slider sliderMusic;
    [SerializeField] private AudioSource moveMainMenuCursor;
    public int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public static OptionsMenuPause instance;
    public bool ezMode = false;
    public GameObject textEzModeON;
    public GameObject textEzModeOFF;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            optionsMenuUI.SetActive(false);
        }

        if (ezMode)
        {
            textEzModeON.SetActive(true);
            textEzModeOFF.SetActive(false);
        } else {
            textEzModeON.SetActive(false);
            textEzModeOFF.SetActive(true);
        }
    }

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            // Full Screen
            FullScreenOptionsPauseMenu();
        }
        else if (SelectedButton == 2)
        {
            // EzMode
            if (textEzModeON.activeSelf)
            {
                ezMode = false;
            }
            else
            {
                ezMode = true;
            }
        }
        else if (SelectedButton == 3)
        {
            // Back
            BackToPauseMenu();
        }
    }

    public void BackToPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void FullScreenOptionsPauseMenu()
    {
        if (Screen.fullScreen)
        {
            Debug.Log("No FullScreen");
            FullScreen(false);
        }
        else
        {
            Debug.Log("FullScreen");
            FullScreen(true);
        }
    }

    // volume
    public void SetMusicVolumen(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetEffectVolumen(float volume)
    {
        audioMixer.SetFloat("EffectVolume", Mathf.Log10(volume) * 20);
    }

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
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
