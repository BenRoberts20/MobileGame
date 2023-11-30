using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;

    void Start()
    {

    }


    //Uses Accelerometer to move.
    void Update()
    {
        movement = new Vector2(Input.acceleration.x, Input.acceleration.y) / 10;
        transform.Translate(movement);
        PreventPlayerOffScreen();
    }

    //Boundaries to prevent player going off screen
    //Using Hard Values currently, but will be changing to the players screen size as the boundary in future.
    private void PreventPlayerOffScreen()
    {
        if (transform.position.y >= 0)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        else if (transform.position.y <= -8)
        {
            transform.position = new Vector2(transform.position.x, -8);
        }

        if (transform.position.x >= 2)
        {
            transform.position = new Vector2(2, transform.position.y);
        }
        else if (transform.position.x <= -2)
        {
            transform.position = new Vector2(-2, transform.position.y);
        }

    }
}
