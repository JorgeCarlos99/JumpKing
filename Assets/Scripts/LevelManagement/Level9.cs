using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    public GameObject canvasText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel3 = new Vector3(-3f, 2530, 0);
            Camera.main.gameObject.transform.position = changeToLevel3;
            if (SpearCounter.instance.spears < 2)
            {
                Debug.Log("You have 2 Spears so the barrier is destroied");
                canvasText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canvasText.SetActive(false);
        }
    }
}
