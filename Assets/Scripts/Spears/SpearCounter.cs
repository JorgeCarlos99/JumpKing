using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCounter : MonoBehaviour
{
    public int spears = 0;
    public int spearsLoaded;

    public static SpearCounter instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            spearsLoaded = SaveManager.instance.activeSave.spears;
            spears = spearsLoaded;

            if (SaveManager.instance.activeSave.lanza1 != null)
            {
                Debug.Log("You get the first spear");
                GameObject.Find("SpearKaladinV1").SetActive(false);
            }
            if (SaveManager.instance.activeSave.lanza2 != null)
            {
                Debug.Log("You get the second spear");
                GameObject.Find("SpearKaladinV2").SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "spear")
        {
            Debug.Log("You catch a spear");
            other.gameObject.SetActive(false);
            spears++;
        }
    }
}
