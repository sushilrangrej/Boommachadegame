using System.Collections;

    void Awake()
    {
        EndlessText.text = PlayerPrefs.GetInt("HighScoreEnd").ToString();
        LevelMode.text = PlayerPrefs.GetInt("HighScore").ToString();
    }