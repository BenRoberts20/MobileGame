using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public GameObject enemyFolder;
    public TMP_Text countdownTxt;
    public TMP_Text enemiesAliveTxt;
    public TMP_Text waveText;
    public Image enemiesAliveImage;
    private float spawnInterval = 1f;
    private float intermissionInterval = 5f;
    private bool inIntermission = false;
    private int maxSpawnCount = 8;
    private int curSpawn = 0;
    private int curWave = 0;
    public bool waveCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentWave")) curWave = PlayerPrefs.GetInt("CurrentWave");
        StartCoroutine(spawnEnemy(intermissionInterval, enemyPrefab));
    }
    private void FixedUpdate()
    {
        UpdateEnemyCount();
        CheckIfCanStartNewWave();
    }
    private void CheckIfCanStartNewWave()
    {
        if (enemyFolder.transform.childCount == 0 && !inIntermission)
        {
            enemiesAliveTxt.gameObject.SetActive(false);
            enemiesAliveImage.gameObject.SetActive(false);
            waveCleared = true;
            PlayerPrefs.SetInt("CurrentWave", curWave);
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if (interval == intermissionInterval)
        {
            float normalizedTime = interval;
            curWave++;
            waveText.text = "Wave " + curWave;
            waveCleared = false;
            inIntermission = true;
            curSpawn = 0;
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
                enemiesAliveTxt.gameObject.SetActive(true);
                enemiesAliveImage.gameObject.SetActive(true);
                enemiesAliveTxt.text = enemyFolder.transform.childCount + "/" + maxSpawnCount;
                inIntermission = false;
                StartCoroutine(spawnEnemy(spawnInterval, enemy));
            }
            else StartCoroutine(spawnEnemy(3f, enemy));
        }
        else StartCoroutine(spawnEnemy(intermissionInterval, enemy));

    }

    public void UpdateEnemyCount()
    {
        enemiesAliveTxt.text = enemyFolder.transform.childCount + "/" + maxSpawnCount;
    }

    public int GetCurrentWave()
    {
        return curWave;
    }

    public void RestartWaves()
    {
        curWave = 0;
        for(int i = 0; i < enemyFolder.transform.childCount; i++)
        {
            Destroy(enemyFolder.transform.GetChild(i).gameObject);
        }
        inIntermission = false;
        CheckIfCanStartNewWave();
    }

}
