using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeGame : MonoBehaviour
{
    public Animator openQuitAnim;
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            openQuitAnim.SetTrigger("OpenQuit");
        }
    }
}
