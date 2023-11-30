using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Projectile;
    private float cooldown = 0.75f;
    private float nextFire = 0.0f;
    //Projectile cooldown + shoot
    public void FireProjectile()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + cooldown;
            Instantiate(Projectile, transform.position, transform.rotation);
        }
    }
}
