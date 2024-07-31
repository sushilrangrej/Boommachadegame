using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarnaToBoom : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(karnaToBoom());
    }

    IEnumerator karnaToBoom()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
