using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    public GameObject bgMusic1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bgMusic1.SetActive(false);
            Vector3 changeToLevel3 = new Vector3(-3f, 1242, 0);
            Camera.main.gameObject.transform.position = changeToLevel3;
        }
    }
}
