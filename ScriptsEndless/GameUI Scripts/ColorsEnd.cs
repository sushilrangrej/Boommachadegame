using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsEnd : MonoBehaviour
{
    public Material[] material;
    Renderer rendere;
    public int random;

    void Start()
    {
        rendere = GetComponentInChildren<Renderer>();
        rendere.enabled = true;
        random = Random.Range(0, material.Length);
        rendere.sharedMaterial = material[random];
    }
}
