using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Camera size
        // Debug.Log("camara" + Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 changeToLevel2 = new Vector2(-3f, 276);
            Camera.main.gameObject.transform.position = changeToLevel2;
        }
    }
}
