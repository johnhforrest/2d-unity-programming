using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    protected Vector2 targetVelocity;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;

    protected const float minMoveDistance = 0.001f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {
        throw new System.NotImplementedException();
    }

    void FixedUpdate()
    {
        velocity.x = targetVelocity.x;
        velocity.y = targetVelocity.y;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Movement(deltaPosition);
    }

    void Movement(Vector2 move)
    {
        float distance = move.magnitude;
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}