using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoomToPlay : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(karnaToBoom());
    }

    IEnumerator karnaToBoom()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
