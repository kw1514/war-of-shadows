using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// calculates the gravity 

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;

    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;  // => returns the object on the left

    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        // turns the nav mesh back on
        if (agent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force) // adds impact forces
    {
        impact += force;

        // turns off the nav mesh for the enemy when getting knockback
        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void Jump(float jumpForce) 
    {
        verticalVelocity += jumpForce;
    }
}
