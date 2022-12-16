using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject bgMusicBirds;
    public GameObject bgMusicCave;
    public GameObject bgMusicClouds;
    public GameObject bgMusicSpace;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bgMusicBirds.SetActive(true);
            bgMusicCave.SetActive(false);
            bgMusicClouds.SetActive(false);
            bgMusicSpace.SetActive(false);
            Vector3 changeToLevel2 = new Vector3(-3f, -47, 0);
            Camera.main.gameObject.transform.position = changeToLevel2;
        }
    }
}
