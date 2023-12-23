using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveprojectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4.0f);
    }

    // Update is called once per frame
    //projectile moves upwards only, then destroys after 2 seconds.
    void Update()
    {
        this.transform.Translate(0f, 5f * Time.deltaTime, 0f);
    }
}
