using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChanges : MonoBehaviour
{
    public PlayerControllerNoPhysics playerControllerNoPhysics;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change the 'color' property of the 'Sprite Renderer'
        if (playerControllerNoPhysics.isInTheGround)
        {
            sprite.color = new Color(0, 1, 0, 1);
        }
        else
        {
            sprite.color = new Color(1, 0, 0, 1);
        }
    }
}
