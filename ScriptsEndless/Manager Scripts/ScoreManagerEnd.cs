using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManagerEnd : MonoBehaviour
{

    public static ScoreManagerEnd instance;

    public Text scoreText;

    public int dies = 1;
    public bool reset;

    public int ScoreN = 0;
    public int targetScore = 1;

    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        MakeSingleton();
    }

    void Start()
    {
        AddScore(0);
    }

    private void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text = ScoreN.ToString();
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
        ScoreN += amount;
        if (ScoreN > PlayerPrefs.GetInt("HighScoreEnd", 0))
            PlayerPrefs.SetInt("HighScoreEnd", PlayerPrefs.GetInt("ScoreEnd") + ScoreN);

        scoreText.text = ScoreN.ToString();
    }
    public void ResetScore()
    {
        if (reset == true)
        {
            ScoreN = 0;
        }
        else
        {
            reset = false;
        }
    }
}
