using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;
    
    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        //if (gameManager.ReturnGameOver()) return;
        if (gameManager.GameOver) return;

        score += amount;
        scoreText.text = score.ToString();
    }
}