using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckBoxPopUp : MonoBehaviour
{
    public GameObject message;
    bool collided = false;
    public float delay = 0.1f;
    private string fullText;
    // private bool done = false;
    private string doneText;
    public TextMeshProUGUI texto;

    void Start()
    {
        fullText = "I did not expect to see another knight, I see that you have also taken the metals that make you jump almost as much as the Lord Jumper. Good luck Nameless Knight....";
    }

    void Update()
    {
        // if (texto.text.Equals(fullTextMin1))
        // {
        //     done = true;
        // }
        // else
        // {
        //     done = false;
        // }
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collided = true; //change collided to true
            yield return new WaitForSeconds(0f); //wait 0.1 seconds
            if (collided == true) //check if collided is still true
            {
                message.SetActive(true);
                StartCoroutine(ShowText());
            }
        }
    }

    //Turns bool collided to false if there is no trigger anymore.
    IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(5);
            message.SetActive(false);
            collided = false;
        }
    }

    IEnumerator ShowText()
    {
        // Debug.Log(" currentText " + currentText);
        // if (done)
        // {
        //     Debug.Log(" currentText1 " + currentText);

        //     currentText = "";
        // }
        // Debug.Log(" currentText1 " + currentText);
        for (int i = 0; i < fullText.Length; i++)
        {
            // currentText = fullText.Substring(0, i);
            // texto.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
