using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float acceleration;
    public GameController gameManager;

    private bool started = false;
    private float startingSpeed = 3f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!started && Input.GetKeyDown("space"))
        {
            // Generating 2 random numbers that are either 1 or -1
            // Based on startingSpeed, we know the magnitude of the vector
            // This will help randomize the direction of the vector
            System.Random random = new System.Random();
            int random1 = random.Next(0, 2) * 2 - 1;
            int random2 = random.Next(0, 2) * 2 - 1;

            rb2d.velocity = new Vector2(random1 * startingSpeed, random2 * startingSpeed);
            started = true;
        }
    }

    private void ResetPosition()
    {
        rb2d.position = Vector2.zero;
        rb2d.velocity = Vector2.zero;
        started = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1Goal"))
        {
            // If it hits player 1's goal, player 2's score increases
            gameManager.IncreasePlayerScore(2);
            ResetPosition();
        }
        else if (collision.gameObject.CompareTag("Player2Goal"))
        {
            // and vice-versa
            gameManager.IncreasePlayerScore(1);
            ResetPosition();
        }
        else
        {
            rb2d.velocity = Vector2.Reflect(rb2d.velocity, collision.transform.right);
            rb2d.velocity *= acceleration;
        }
    }
}
