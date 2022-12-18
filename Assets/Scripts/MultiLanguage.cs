using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class MultiLanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        // switch (Application.systemLanguage)
        // {
        //     case SystemLanguage.English:
        //         LocalizationManager.Language = "English";
        //         break;
        //     case SystemLanguage.Spanish:
        //         LocalizationManager.Language = "Spanish";
        //         break;
        // }
    }

    public void Language(string Language) {
        LocalizationManager.Language = Language;
    }
}
