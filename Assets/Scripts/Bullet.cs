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
        string colTag = collision.gameObject.tag;
        Health H = collision.gameObject.GetComponent<Health>();

        if (colTag == owner || colTag == "Flag" || colTag == "Weapon") return;

        if (H != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
