using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private const float Force = 0.2f;
    private float m_DistToGround;

    private Camera m_Camera;


    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        m_DistToGround = GetComponent<Collider>().bounds.extents.y;
    }
    
    bool IsGrounded()
    {
        var bounds = GetComponent<Collider>().bounds;
        return Physics.CheckCapsule(bounds.center,
            new Vector3(bounds.center.x, bounds.min.y - 0.1f,
                bounds.center.z), 0.18f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.0f, vertical = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1.0f;
        }

        // jump
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Force, ForceMode.Impulse);
        }

        // refactor this part out for later uses
        var camTransform = m_Camera.transform;
        var forward = camTransform.forward;
        var right = camTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        var desiredMoveDirection = forward * vertical + right * horizontal;

        GetComponent<Rigidbody>().AddForce(desiredMoveDirection * Force, ForceMode.Impulse);
        
        // camera look at player
        m_Camera.transform.LookAt(transform);
    }
}