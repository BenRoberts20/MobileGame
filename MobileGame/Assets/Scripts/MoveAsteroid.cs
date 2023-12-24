using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
    public bool canMove = true;
    void Start()
    {
        Destroy(this.gameObject, 10.0f);
    }
    void Update()
    {
        if (canMove)
        {
            gameObject.transform.Translate(0f, -4f * Time.deltaTime, 0f);
        }
    }
}
