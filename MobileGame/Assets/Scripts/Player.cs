using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int Health = 10;
    private int Damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {

    }

    public void DealDamage(GameObject HitEnemy)
    {
        HitEnemy.GetComponent<Enemy>().Health -= Damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            //Take Damage
        }
    }
}
