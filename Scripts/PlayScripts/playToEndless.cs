using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playToEndless : MonoBehaviour
{
    public void PlayEndless()
    {
        SceneManager.LoadScene(4);
    }
}
