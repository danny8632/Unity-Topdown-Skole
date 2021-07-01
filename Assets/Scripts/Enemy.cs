using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Movement mov;
    private Shoot shoot;
    private int waypointIndex = 0;
    private bool canSeePlayer = false;
    private bool isShooting = false;

    public float shootDelay = 3f;
    public float viewDistance = 15f;
    public LayerMask layermask;
    public Transform[] waypoints;
    public Transform player;
 
    void Start()
    {
        mov = gameObject.GetComponent<Movement>();
        shoot = gameObject.GetComponent<Shoot>();
    }

    private void FixedUpdate()
    {
        if (canSeePlayer)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void LookForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, viewDistance, layermask);

        canSeePlayer = hit.collider != null && hit.collider.gameObject.tag == "Player";
    }

    IEnumerator ValidatePlayer()
    {
        yield return new WaitForSeconds(1f);
        LookForPlayer();
    }

    IEnumerator shootAtPlayer()
    {
        isShooting = true;
        shoot.shoot(gameObject.tag);
        yield return new WaitForSeconds(2f);
        isShooting = false;
    }

    void AttackPlayer()
    {
        mov.LookAtPos(player.position, -90f);

        if(Vector2.Distance(transform.position, player.position) > 4f)
        {
            mov.MoveByPos(player.position);
        }

        if (isShooting == false)
        {
            StartCoroutine(shootAtPlayer());
        }

        StartCoroutine(ValidatePlayer());
    }

    void Patrol()
    {
        mov.LookAtPos(waypoints[waypointIndex].position, -90f);
        mov.MoveByPos(waypoints[waypointIndex].position);

        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
        LookForPlayer();
    }
}
