using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playToLevelMode : MonoBehaviour
{

    public void PlayToLevelMode()
    {
        SceneManager.LoadScene(3);
    }
}
