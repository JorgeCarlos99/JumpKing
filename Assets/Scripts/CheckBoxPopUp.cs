using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxPopUp : MonoBehaviour
{
    public GameObject message;
    bool collided = false;

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collided = true; //change collided to true
            yield return new WaitForSeconds(0.1f); //wait 0.1 seconds
            if (collided == true) //check if collided is still true
            {
                message.SetActive(true);
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
}
