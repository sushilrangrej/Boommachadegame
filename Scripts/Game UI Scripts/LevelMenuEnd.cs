using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuEnd : MonoBehaviour
{
    public Animator quitGameOption;
    public void backToMenuEnd()
    {
        SceneManager.LoadScene(2);
        ScoreManagerEnd.instance.ScoreN = 0;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quitGameOption.SetTrigger("OpenQuit");
        }
    }
}
