using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnDelay = 3f;
    [SerializeField] float minObstacleSpawnTime= 0.5f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstacelRoutine());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        if (obstacleSpawnDelay <= minObstacleSpawnTime) return;

        obstacleSpawnDelay -= amount;
    }

    IEnumerator SpawnObstacelRoutine()
    {
        while (true) 
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPos = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            yield return new WaitForSeconds(obstacleSpawnDelay);
            Instantiate(obstaclePrefab, spawnPos, Random.rotation, obstacleParent);
        }
    }
}
