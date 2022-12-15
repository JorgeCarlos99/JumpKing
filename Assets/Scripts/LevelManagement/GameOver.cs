using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject timeLine;
    public SpearCounter spearCounter;
    public float changeTime;
    public string sceneName;
    private bool insideCheckBoxFinal;
    public GameObject AppearsBoxColliderBehindPlayer;

    // Update is called once per frame
    void Update()
    {
        if (insideCheckBoxFinal)
        {
            changeTime -= Time.deltaTime;
            if (changeTime <= 0)
            {
                SceneManager.LoadScene(sceneName);
                SaveManager.instance.DeleteSavedData();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && SpearCounter.instance.spears >= 2)
        {
            Debug.Log("gg");
            timeLine.SetActive(true);
            AppearsBoxColliderBehindPlayer.SetActive(true);
            insideCheckBoxFinal = true;
        } else {
           AppearsBoxColliderBehindPlayer.SetActive(false); 
        }
    }
}
