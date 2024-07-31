
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class diskSpeed : MonoBehaviour
{
    public static diskSpeed instance;
    public static int speed = 100;
    public Text value;

    void Start()
    {
        if(speed == 0)
        {
            speed = 100;
        }
        value.text = PlayerPrefs.GetInt("speed").ToString();
        speed = PlayerPrefs.GetInt("speed");
    }

    public void easy()
    {
        int values = 100;
        value.text = values.ToString();

        PlayerPrefs.SetInt("speed", values);
        speed = values;
        Debug.Log(PlayerPrefs.GetInt("speed", values));
    }

    public void medium()
    {
        int values = 150;
        value.text = values.ToString();

        PlayerPrefs.SetInt("speed", values);
        speed = values;
        Debug.Log(PlayerPrefs.GetInt("speed", values));
    }

    public void hard()
    {
        int values = 250;
        value.text = values.ToString();

        PlayerPrefs.SetInt("speed", values);
        speed = values;
        Debug.Log(PlayerPrefs.GetInt("speed", values));
    }

    public void extreme()
    {
        int values = 350;
        value.text = values.ToString();

        PlayerPrefs.SetInt("speed", values);
        speed = values;
        Debug.Log(PlayerPrefs.GetInt("speed", values));
    }
}
