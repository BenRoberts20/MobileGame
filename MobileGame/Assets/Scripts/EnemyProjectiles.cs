using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    public bool isHoming = false;
    private float FollowTimer = 4f;
    private float projSpeed = 1.5f;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (isHoming)
        {
            Destroy(this.gameObject, 15.0f);
        }
        else Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        if (isHoming)
        {
            if (FollowTimer > 0f)
            {
                Vector2 direction = player.transform.position - this.transform.position;
                direction.Normalize();
                //this.transform.LookAt(player.transform);
                this.transform.Translate((direction * projSpeed) * Time.deltaTime);
                FollowTimer -= Time.deltaTime;
            }
            else isHoming = false;
        }
        else this.transform.Translate(0f, -3f * Time.deltaTime, 0f);
    }
}
