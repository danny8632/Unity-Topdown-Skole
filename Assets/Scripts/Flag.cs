using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Transform playerFlagPoint;

    private void FixedUpdate()
    {
        if(GameController.instance.IsFlagTaken())
        {
            transform.position = playerFlagPoint.position;

            Quaternion playerRotation = playerFlagPoint.parent.transform.rotation;
            playerRotation *= Quaternion.Euler(0, 0, 90);

            transform.rotation = playerRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameController.instance.IsFlagTaken()) return;

            playerFlagPoint = collision.gameObject.transform.GetChild(2).transform;

            GameController.instance.TakeFlag(true);
        }
    }
}
