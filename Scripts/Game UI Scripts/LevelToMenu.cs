using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToMenu : MonoBehaviour
{
    public Animator quitGameOption;
    public void backToMenu()
    {
        SceneManager.LoadScene(2);
        ScoreManager.instance.score = 0;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            quitGameOption.SetTrigger("OpenQuit");
        }
    }
}
