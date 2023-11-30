using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public GameObject enemyFolder;
    public TMP_Text countdownTxt;
    public TMP_Text enemiesAliveTxt;
    private float spawnInterval = 2f;
    private float intermissionInterval = 5f;
    private int maxSpawnCount = 8;
    private int curSpawn = 0;
    public int curWave = 0;
    public bool waveCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(intermissionInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if (interval == intermissionInterval)
        {
            float normalizedTime = interval;
            while (normalizedTime > 0)
            {
                normalizedTime -= Time.deltaTime;
                countdownTxt.text = "Wave starts in: " + Mathf.Round(normalizedTime * 100f) / 100f;
                yield return null;
            }
        }
        else yield return new WaitForSeconds(interval);

        if (!waveCleared)
        {
            if (curSpawn < maxSpawnCount)
            {
                countdownTxt.text = "Wave in progress";
                GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), 0), Quaternion.identity);
                newEnemy.GetComponent<Enemy>().SpawnID = curSpawn;
                newEnemy.transform.parent = enemyFolder.transform;
                curSpawn++;
                enemiesAliveTxt.GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
                enemiesAliveTxt.text = "Enemies Alive: " + enemyFolder.transform.childCount + "/" + maxSpawnCount;
                StartCoroutine(spawnEnemy(spawnInterval, enemy));
            }
            else StartCoroutine(spawnEnemy(3f, enemy));
        }
        else StartCoroutine(spawnEnemy(intermissionInterval, enemy));

    }
}
