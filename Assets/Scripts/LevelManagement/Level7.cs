using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel3 = new Vector3(-3f, 1886, 0);
            Camera.main.gameObject.transform.position = changeToLevel3;
        }
    }
}
