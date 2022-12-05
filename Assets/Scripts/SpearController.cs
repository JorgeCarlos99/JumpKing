using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    private int spears = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Destroy(gameObject);
            spears ++;
            Debug.Log("Lanzas: "+spears);
        }
    }
}
