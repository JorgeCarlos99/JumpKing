using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level22 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel2 = new Vector3(-3f, 275, 0);
            Camera.main.gameObject.transform.position = changeToLevel2;
        }
    }
}
