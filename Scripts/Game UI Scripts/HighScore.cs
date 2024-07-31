using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class HighScore : MonoBehaviour{	public Text EndlessText;	public Text LevelMode;

    void Awake()
    {
        EndlessText.text = PlayerPrefs.GetInt("HighScoreEnd").ToString();
        LevelMode.text = PlayerPrefs.GetInt("HighScore").ToString();
    }}