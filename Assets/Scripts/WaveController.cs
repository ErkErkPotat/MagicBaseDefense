using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    public float spawnRadius = 25f;
    private int enemiesAlive = 0;
    private bool finishedSpawning = false;

    public GameObject BaseHuntingEnemy;
    public Transform target;

    void Start()
    {
        StartCoroutine(SpawnWave(5));
    }

    void SpawnEnemy()
    {
        float randomDirDeg = Random.Range(0f, 360f);
        float randomDirRad = randomDirDeg * Mathf.Deg2Rad; 

        float randomDirSin = Mathf.Sin(randomDirRad);
        float randomDirCos = Mathf.Cos(randomDirRad);

        float xOffset = randomDirCos*spawnRadius;
        float zOffset = randomDirSin*spawnRadius;

        Vector3 spawnPosition = transform.position + new Vector3(xOffset, 0f, zOffset);
        GameObject spawnedEnemy = Instantiate(BaseHuntingEnemy, spawnPosition, Quaternion.identity);
        enemiesAlive++;

        BasehunterController hunterController = spawnedEnemy.GetComponent<BasehunterController>();

        hunterController.target = target;
        hunterController.waveController = this;
    }

    IEnumerator SpawnWave(int enemyCount)
    {
        yield return new WaitForSeconds(5f);
        finishedSpawning = false;
        for (int i = 0; i < enemyCount; i++)
        {          
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
        finishedSpawning = true;
        CheckWaveComplete();
    }

    public void EnemyKilled () {
        enemiesAlive--;
        CheckWaveComplete();
    }

    void CheckWaveComplete()
    {
        if (finishedSpawning && enemiesAlive == 0)
        {
            Debug.Log("Wave complete!");
        }
    }
}
