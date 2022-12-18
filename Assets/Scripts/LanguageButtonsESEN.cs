using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class LanguageButtonsESEN : MonoBehaviour
{
    public GameObject buttonESNoActivate;
    public GameObject buttonESActivate;
    public GameObject buttonENNoActivate;
    public GameObject buttonENActivate;

    // Update is called once per frame
    void Update()
    {
        if (LocalizationManager.Language == "English")
        {
            buttonESNoActivate.SetActive(true);
            buttonESActivate.SetActive(false);
            buttonENNoActivate.SetActive(false);
            buttonENActivate.SetActive(true);
        }
        if (LocalizationManager.Language == "Spanish")
        {
            buttonESNoActivate.SetActive(false);
            buttonESActivate.SetActive(true);
            buttonENNoActivate.SetActive(true);
            buttonENActivate.SetActive(false);
        }
    }
}
