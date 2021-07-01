using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
