using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCounter : MonoBehaviour
{
    public int spears = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "spear")
        {
            Destroy(other.gameObject);
            spears++;
        }
    }
}
