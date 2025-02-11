using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] float checkPointTimeExtension = 5f;
    [SerializeField] float obstacleDecreaseTimeAmount = 0.2f;

    GameManager gameManager;
    
    ObstacleSpawner obstacleSpawner;

    const string playerString = "Player";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); 
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkPointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }
}
 