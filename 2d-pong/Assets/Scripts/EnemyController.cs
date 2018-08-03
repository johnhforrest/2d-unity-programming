using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Transform ballTransform;

    private Rigidbody2D rb2d;
    private float minDistance = 0.5f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ballTransform.position.y > rb2d.position.y + minDistance)
        {
            rb2d.AddForce(new Vector2(0, speed));
        }
        else if (ballTransform.position.y < rb2d.position.y - minDistance)
        {
            rb2d.AddForce(new Vector2(0, -speed));
        }
    }
}
