using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement mov;
    private Shoot shoot;


    void Start()
    {
        mov = gameObject.GetComponent<Movement>();
        shoot = gameObject.GetComponent<Shoot>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot.shoot(gameObject.tag);
        }
    }

    void FixedUpdate()
    {
        mov.LookAtPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mov.MoveByInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
