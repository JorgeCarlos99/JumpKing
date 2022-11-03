using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 changeToLevel2 = new Vector3(-3f, -47, -10);
            Camera.main.gameObject.transform.position = changeToLevel2;
        }
    }
    // IEnumerator DelayBeforeChangeCameraPosition(Vector3 changeToLevel2)
    // {
    //     Debug.Log("Delaying");
    //     yield return new WaitForSecondsRealtime(0.1f);
    //     Camera.main.gameObject.transform.position = changeToLevel2;
    // }
}
