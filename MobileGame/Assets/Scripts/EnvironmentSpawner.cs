using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject asteroid;
    void Start()
    {
        StartCoroutine(SpawnAsteroid(asteroid));
    }

    private IEnumerator SpawnAsteroid(GameObject obj)
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Instantiate(obj, new Vector3(Random.Range(-2f, 2f), 6f, 0), Quaternion.identity);
        StartCoroutine(SpawnAsteroid(obj));
    }
}
