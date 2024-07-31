using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowEnd : MonoBehaviour
{
    private Vector3 camFollow;
    private Transform player, win;

    void Awake()
    {
        //if (player == null)
            //player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<Transform>();

        if (transform.position.y > player.transform.position.y)
            camFollow = new Vector3(transform.position.x, player.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, camFollow.y, -3.2f);
    }
}
