using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartControllerEnd : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MeshRenderer meshRender;
    private StackController stackController;
    private Collider colliders;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRender = GetComponent<MeshRenderer>();
        stackController = transform.parent.GetComponent<StackController>();
        colliders = GetComponent<Collider>();
    }

    public void Shatter()
    {
        rigidBody.isKinematic = false;//diables the kinametics
        colliders.enabled = false;//disables collider to does interact one with another
        
        Vector3 forcePoint = transform.parent.position;//force point at the cemnter of the object 0
        float paretXpos = transform.parent.position.x;//piviot point of the object which is at the center
        float xPos = meshRender.bounds.center.x;//individual pieces x position

        Vector3 subDir = (paretXpos - xPos < 0) ? Vector3.right : Vector3.left;//true right side if false left side
        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;//1.5f becz we are multiplaying it for faster and subfir means which direction it gone go normalized means only one force act on the gameobject

        float force = Random.Range(3,8);
        float torque = Random.Range(100, 180);

        rigidBody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);//Only once it adds force on the gameobject
        rigidBody.AddTorque(Vector3.left * torque);
        rigidBody.velocity = Vector3.down;//rotating every single part in this direction
    }
}
