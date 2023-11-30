using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject spawner;
    Transform target;

    public int Health = 5;
    public int Damage = 2;
    public int SpawnID = 0;
    // Start is called before the first frame update
    //finds the target
    void Start()
    {
        spawner  = GameObject.Find("EnemySpawner");
        if (spawner.transform.childCount >= SpawnID) target = spawner.transform.GetChild(SpawnID);
    }

    // Update is called once per frame
    //moves towards target
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3f * Time.deltaTime);

        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
