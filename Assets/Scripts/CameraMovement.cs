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


    public IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 origPos = transform.position;

        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.position = new Vector3(origPos.x - xOffset, origPos.y - yOffset, origPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = origPos;
    }
}
