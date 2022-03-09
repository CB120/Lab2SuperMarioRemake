using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private Text scoreText, coinText;
    public int score = 0;
    public int coinScore = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = "Mario\n" + score.ToString("000000");
    }

    public void IncreaseCoin()
    {
        coinScore += 1;
        coinText.text = "x" + coinScore.ToString("00");
    }
}
