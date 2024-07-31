using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteData : MonoBehaviour
{
    public HighScore Endless;
    public void DeleteAllData()
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("HighScoreEnd");
        Endless.EndlessText.text = 0.ToString();
        Endless.LevelMode.text = 0.ToString();
        //Debug.Log(PlayerPrefs.GetInt("level"));
        //Debug.Log(PlayerPrefs.GetInt("HighScore"));
        //Debug.Log(PlayerPrefs.GetInt("HighScoreEnd"));
    }
}
