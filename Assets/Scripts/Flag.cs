using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private bool isCaptured = false;

    public Transform playerFlagPoint;


    private void FixedUpdate()
    {
        if(isCaptured)
        {
            transform.position = playerFlagPoint.position;

            Quaternion playerRotation = playerFlagPoint.parent.transform.rotation;
            playerRotation *= Quaternion.Euler(0, 0, 90);

            transform.rotation = playerRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCaptured) return;

        if (collision.gameObject.tag == "Player")
        {
            isCaptured = true;
        }

    }
}
