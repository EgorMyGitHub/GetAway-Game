using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float damping;
    [SerializeField] private float distance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedRotate;
    
    [SerializeField] public Transform gunPos;
    [SerializeField] private Transform legsPos;
    [SerializeField] private Transform centerOfMass;

    [SerializeField] private LayerMask whatIsGround;
    
    private float m_InputX;
    
    private Rigidbody2D m_Rb;

    private bool m_IsSpace;

    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        GetAxis();
        Rotate();
        Jump();

        if (GetIsGround() && Math.Abs(transform.rotation.eulerAngles.z) < 50)
        {
            if (m_InputX > 0)
                transform.Rotate(Vector3.forward * m_InputX * speedRotate * Time.deltaTime);
            else if(m_InputX < 0)
                transform.Rotate(Vector3.back * m_InputX * speedRotate * Time.deltaTime);
        }

        m_Rb.centerOfMass = centerOfMass.localPosition;
    }
    
    private void Jump()
    {
        GetIsGround();
        
        if (m_IsSpace && GetIsGround())
        {
            m_Rb.AddForce(transform.up * jumpForce);
        }
    }
    
    private void Rotate()
    {
        if (!GetIsGround())
        {
            m_Rb.freezeRotation = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * damping);
        }
        else
        {
            m_Rb.freezeRotation = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(centerOfMass.position, 0.1f);
    }

    private void GetAxis()
    {
        m_InputX = Input.GetAxis("Horizontal");
        m_IsSpace = Input.GetKeyDown(KeyCode.Space);
    }

    private bool GetIsGround()
    {
        return Physics2D.Raycast(legsPos.position, Vector2.down, distance, whatIsGround);
    }
}
