using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeToSelfDestroy = 5f;

    public float damage = 10f;

    public string owner = "";

    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(timeToSelfDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == owner) return;

        collision.gameObject.GetComponent<Health>().TakeDamage(damage);

        Destroy(gameObject);
    }
}
