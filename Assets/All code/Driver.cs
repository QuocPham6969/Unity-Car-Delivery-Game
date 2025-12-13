using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float SteeringSpeed = 0.2f;
    [SerializeField] float MoveSpeed = 0.01f;

    [Header("Knockback")]
    [SerializeField] float knockbackSpeed = 4f;   // how fast player gets pushed
    private float knockbackTimer = 0f;
    private Vector3 knockbackDirection;

    void Update()
    {
        // If we are in knockback / stun, move by knockback and ignore input
        if (knockbackTimer > 0f)
        {
            transform.Translate(knockbackDirection * knockbackSpeed * Time.deltaTime, Space.World);
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer < 0f) knockbackTimer = 0f;
            return; // no steering / throttle while stunned
        }

        
        float SteerAmount = Input.GetAxis("Horizontal") * SteeringSpeed * Time.deltaTime;
        float MoveAmount = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -SteerAmount);
        transform.Translate(0, MoveAmount, 0);
    }

    // Called by CarAI when collision happens
    public void ApplyKnockback(Vector3 direction, float duration)
    {
        knockbackDirection = direction.normalized;
        knockbackTimer = duration;
    }
}
