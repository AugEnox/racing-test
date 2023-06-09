using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    public float steering;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Input.GetAxis("Horizontal") * steering, 0, Input.GetAxis("Vertical") * force);
        if (rb.velocity.magnitude > 2.5f)
            transform.Rotate(0, Input.GetAxis("Horizontal") * steering, 0);
    }
}
