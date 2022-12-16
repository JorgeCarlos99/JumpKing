using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    public GameObject bgMusicBirds;
    public GameObject bgMusicCave;
    public GameObject bgMusicClouds;
    public GameObject bgMusicSpace;
    public GameObject bgMusicBoss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel3 = new Vector3(-3f, 2208, 0);
            Camera.main.gameObject.transform.position = changeToLevel3;
            bgMusicBirds.SetActive(false);
            bgMusicCave.SetActive(false);
            bgMusicClouds.SetActive(false);
            bgMusicSpace.SetActive(true);
            bgMusicBoss.SetActive(false);
        }
    }
}
