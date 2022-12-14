using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SureMenu : MonoBehaviour
{
    public GameObject Point;
    public int SelectedButton = 2;
    [SerializeField]
    private int NumberOfButtons;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public GameObject mainMenu;
    public GameObject sureMenu;

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            try
            {
                mainMenu.gameObject.SetActive(true);
                sureMenu.gameObject.SetActive(false);
                SaveManager.instance.DeleteSavedData();
            }
            catch (Exception e)
            {
                Debug.LogError("ERROR AL BORRAR ->" + e);
            }
        }
        else if (SelectedButton == 2)
        {
            mainMenu.gameObject.SetActive(true);
            sureMenu.gameObject.SetActive(false);
        }
    }
    private void OnButtonLeft()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (SelectedButton > 1)
        {
            SelectedButton -= 1;
        }
        MoveThePointer();
        return;
    }
    private void OnButtonRight()
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
    }
}
