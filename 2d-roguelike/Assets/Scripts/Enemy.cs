using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        base.Start();
    }

    protected override void AttemptMove<T>(int xDirection, int yDirection)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDirection, yDirection);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDirection = 0;
        int yDirection = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            // If we're in the same column as the player, move vertically
            yDirection = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            // Else, move horizontally towards the player's column
            xDirection = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDirection, yDirection);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        animator.SetTrigger("enemyAttack");

        hitPlayer.LoseFood(playerDamage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
