using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public GameObject bgMusicBirds;
    public GameObject bgMusicCave;
    public GameObject bgMusicClouds;
    public GameObject bgMusicSpace;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel4 = new Vector3(-3f, 920, 0);
            Camera.main.gameObject.transform.position = changeToLevel4;
            bgMusicBirds.SetActive(false);
            bgMusicCave.SetActive(true);
            bgMusicClouds.SetActive(false);
            bgMusicSpace.SetActive(false);
        }
    }
}
