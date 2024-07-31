using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackControllerEnd : MonoBehaviour
{
    [SerializeField]
    private StackPartControllerEnd[] stackPartControlls = null;

    public void ShatterAllParts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<PlayerEnd>().IncreaseBrokenStacks();
        }

        foreach (StackPartControllerEnd o in stackPartControlls)
        {
            o.Shatter();
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
