using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionsDropDown;
    public GameObject options;
    public GameObject menu;
    Resolution[] resolutions;
    public GameObject Point;
    public Slider sliderEffect;
    public Slider sliderMusic;
    public AudioMixer audioMixer;
    public int SelectedButton = 2;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public static PauseMenuScriptMove instance;

    #region MoveOptionsMenu
    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            // FullScreen
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
        else if (SelectedButton == 2)
        {
            options.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
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

    #endregion

    // void Start()
    // {
    //     ReviewResolutions();
    // }

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = currentResolutionIndex;
        resolutionsDropDown.RefreshShownValue();

        // float volumeEffectValue;
        // bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);

        // if (resultEffect)
        // {
        //     Debug.Log("select music value" + volumeEffectValue);
        //     sliderEffect.SetValueWithoutNotify(volumeEffectValue);
        //     //audioMixer.SetFloat("EffectVolume", volumeEffectValue);
        // }
        // else
        // {
        // }

    }

    // private void Update()
    // {
    //     if (options.activeSelf)
    //     {
    //         float volumeMusicValue;
    //         bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
    //         Debug.Log("respuesta: " + Mathf.Pow(10, 2));
    //         if (resultMusic)
    //         {
    //             // sliderMusic.SetValueWithoutNotify(Mathf.Pow(10, volumeMusicValue));
    //             sliderMusic.SetValueWithoutNotify(-15);
    //             Debug.Log("select music value" + Mathf.Pow(10, volumeMusicValue));
    //             //audioMixer.SetFloat("MusicVolume", volumeMusicValue);
    //         }
    //         else
    //         {
    //         }
    //     }

    // }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // volume
    public void SetMusicVolumen(float volume)
    {
        Debug.Log("volumen: " + volume);
        Debug.Log("volumen 1 : " + Mathf.Log10(volume));
        Debug.Log("volumen 2 : " + Mathf.Log10(volume) * 20);

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetEffectVolumen(float volume)
    {
        audioMixer.SetFloat("EffectVolume", Mathf.Log10(volume) * 20);
    }

    // Graphics
    public void SetQuality(int quialityIndex)
    {
        QualitySettings.SetQualityLevel(quialityIndex);
    }
}
