using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private Shoot shoot;

    public float speed = 6.0f;


    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        shoot = gameObject.GetComponent<Shoot>();
    }

    
    public void LookAtPos(Vector2 pos, float rotation = 0)
    {
        if (body == null) return;

        Vector2 lookdir = pos - body.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg + rotation;
        body.rotation = angle;
    }


    public void MoveByInput(float x = 0, float y = 0)
    {
        if (shoot == null || shoot.shooting) return;

        Vector2 pos = Vector2.zero;
        pos.x = x;
        pos.y = y;
        pos.Normalize();
        pos *= Time.deltaTime * speed;

        if(anim != null)
        {
            anim.SetBool("Walking", x != 0 || y != 0);
        }
        body.MovePosition(body.position + pos);
    }

    public void MoveByPos(Vector2 pos)
    {
        if (shoot.shooting) return;

        if (anim != null)
        {
            anim.SetBool("Walking", Vector2.Distance(transform.position, pos) > 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
}
