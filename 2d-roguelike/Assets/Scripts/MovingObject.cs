using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    // Time in seconds
    public float moveTime = 0.1f;

    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private float inverseMoveTime;

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        // This is an optimization trick so we can later multiply instead of divide for each calculation
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(int xDirection, int yDirection, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDirection, yDirection);

        // Disable this boxCollider so that when casting a line from this gameObject a self-collision is not detected
        boxCollider.enabled = false;

        hit = Physics2D.Linecast(start, end, blockingLayer);

        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // Computationally cheaper to use square magnitude instead of raw magnitude
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            // Wait for the next frame before evaluating again
            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDirection, int yDirection)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDirection, yDirection, out hit);

        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;

    // Update is called once per frame
    void Update()
    {

    }
}
