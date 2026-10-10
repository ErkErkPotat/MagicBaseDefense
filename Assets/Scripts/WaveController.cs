using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float spawnRadius = 25f;
    public GameObject BaseHuntingEnemy;
    public Transform target;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5f, 5f);
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
        BasehunterController _hunterController = spawnedEnemy.GetComponent<BasehunterController>();
        _hunterController.target = target;
    }
}
