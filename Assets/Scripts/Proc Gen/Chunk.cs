using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };

    LevelGenerator levelGenerator;
    ScoreManager scoreManager;

    List<int> availableLanes = new List<int> { 0, 1, 2 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }   
    
    private void SpawnFences()
    {
        int fenceToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fenceToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int selectedLane = SelectLane();

            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    int SelectLane()
    {
        int randomIdx = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomIdx];
        availableLanes.RemoveAt(randomIdx);
        return selectedLane;
    }

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        GameObject newApple =  Instantiate(applePrefab, spawnPos, Quaternion.identity, transform);
        newApple.GetComponent<Apple>().Init(levelGenerator);
    }

    void SpawnCoins()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);  

        for(int i=0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos  - (i*coinSeperationLength);
            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }

    }

}
