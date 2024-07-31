using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorEnd : MonoBehaviour
{
    public float speed = 100;
    private void Awake()
    {
        if (diskSpeed.speed == 0)
        {
            diskSpeed.speed = 100;
        }
        speed = diskSpeed.speed;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
