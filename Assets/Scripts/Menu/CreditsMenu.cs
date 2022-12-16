using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject Point;
    public GameObject creditsMenu;
    public GameObject mainMenu;
    public int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    
    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            // Volver al menu principal
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    private void MoveThePointer()
    {
        // Moves the pointer
        if (SelectedButton == 1)
        {
            Point.transform.position = ButtonPosition1.position;
        }
    }
}
