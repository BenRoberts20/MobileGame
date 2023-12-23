using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject spawner;
    Transform target;
    public EnemyCreator obj;
    private int Health = 0;
    private int Damage = 0;
    public int SpawnID = 0;
    private int TillHoming = 3;
    public GameObject enemyProjectile;
    // Start is called before the first frame update
    //finds the target
    void Start()
    {
        spawner = GameObject.Find("EnemySpawner");
        int curWave = spawner.GetComponent<EnemySpawner>().GetCurrentWave();
        Health = (obj.Health * curWave);
        Damage = (obj.Damage * curWave);
        if (spawner.transform.childCount >= SpawnID) target = spawner.transform.GetChild(SpawnID);
        StartCoroutine(ShootProjectile(enemyProjectile));
    }

    // Update is called once per frame
    //moves towards target
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3f * Time.deltaTime);

        if (Health <= 0)
        {
            GameObject p = GameObject.Find("Player");
            p.GetComponent<Player>().IncreaseExp(obj.ExpDrop);
            p.GetComponent<Player>().IncreaseFragments(obj.FragmentsDrop);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Projectile")
        {
            GameObject Player = GameObject.Find("Player");
            Health -= Player.GetComponent<Player>().GetDamage();
            Destroy(col.gameObject);
        }
    }

    public int GetDamage()
    {
        return Damage;
    }

    private IEnumerator ShootProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(Random.Range(4f, 6f));
        TillHoming -= 1;
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        if (TillHoming == 0)
        {
            proj.GetComponent<EnemyProjectiles>().isHoming = true;
            TillHoming = 3;
        }
        StartCoroutine(ShootProjectile(projectile));
    }
}
