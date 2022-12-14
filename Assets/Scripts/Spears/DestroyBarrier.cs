using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrier : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if ( SpearCounter.instance.spears >= 2) {
            Debug.Log("You have 2 Spears so the barrier is destroied");
            Destroy(canvas);
        }
    }
}
