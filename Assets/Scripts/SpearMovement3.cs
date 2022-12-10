using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMovement3 : MonoBehaviour
{
    public float amp;
    public float freq;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, Mathf.Sin(Time.time * freq) * amp + initialPosition.y, 1);
    }
}
