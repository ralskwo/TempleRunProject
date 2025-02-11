using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver= false;

    public bool GameOver => gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
    }

    public bool ReturnGameOver()
    {
        return gameOver;
    }

    private void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    public void IncreaseTime(float amount) 
    {
        timeLeft += amount;
    }

    private void PlayerGameOver()
    {
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;

        gameOver = true;
    }
}
