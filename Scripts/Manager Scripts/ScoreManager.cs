using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int dies = 0;

    private Text scoreText; 

    public int score = 0;

    public bool reset;
    
    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        MakeSingleton();
    }

    void Start()
    {
        AddScore(0);
    }

    void Update()
    {
        if(scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            AddScore(0);//Score is getting zero
        }
    }

    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score") + score);

        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        if(reset == true)
        {
            score = 0;
        }
        else
        {
            reset = false;
        }
    }
}
