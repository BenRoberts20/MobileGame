using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveprojectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //projectile moves upwards only, then destroys after 2 seconds.
    void Update()
    {
        this.transform.Translate(0f, 0.1f, 0f);
        Destroy(this.gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Player>().DealDamage(col.gameObject);
        }
    }
}
