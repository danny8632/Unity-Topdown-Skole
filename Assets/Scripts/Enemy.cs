using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Movement mov;
    private Shoot shoot;
    private int waypointIndex = 0;
    public bool canSeePlayer = false;
    public bool isShooting = false;
    public bool agro = false;

    public float shootDelay = 2f;
    public float viewDistance = 15f;
    public LayerMask layermask;
    public Transform[] waypoints;
 
    void Start()
    {
        mov = gameObject.GetComponent<Movement>();
        shoot = gameObject.GetComponent<Shoot>();
    }

    private void FixedUpdate()
    {
        if (GameController.instance.UIScreenShown) return;

        if (canSeePlayer || GameController.instance.IsFlagTaken())
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

        canSeePlayer = agro || (hit.collider != null && hit.collider.gameObject.tag == "Player");
    }

    IEnumerator ValidatePlayer()
    {
        yield return new WaitForSeconds(2f);
        LookForPlayer();
    }

    IEnumerator shootAtPlayer()
    {
        isShooting = true;
        shoot.shoot();
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
    }

    void AttackPlayer()
    {
        if (GameController.instance.player == null) return;

        mov.LookAtPos(GameController.instance.player.transform.position, -90f);

        if(Vector2.Distance(transform.position, GameController.instance.player.transform.position) > 4f)
        {
            mov.MoveByPos(GameController.instance.player.transform.position);
        }

        if (isShooting == false && canSeePlayer)
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

    public void GotHit()
    {
        StartCoroutine( Agro() );
    }

    IEnumerator Agro()
    {
        agro = true;
        yield return new WaitForSeconds(10f);
        agro = false;
    }
}
