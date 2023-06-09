using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Quaternion targetrot;
    public Vector3 offset;
    public float movespeed;
    public float speed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (WheelSpin.reverse)
        {
            Vector3 tempvector = target.rotation.eulerAngles;
            targetrot = Quaternion.Euler(-tempvector.x, tempvector.y + 180, -tempvector.z);
            speed = movespeed / 2;
        }
        else
        {
            targetrot = target.rotation;
            speed = movespeed;
        }
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothposition = Vector3.Lerp(transform.position, desiredposition, speed);
        transform.position = smoothposition;

        Quaternion smoothrotate = Quaternion.Lerp(transform.rotation, targetrot, speed);
        transform.rotation = smoothrotate;
    }

}
