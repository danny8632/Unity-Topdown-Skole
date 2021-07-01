using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    private Animator anim;

    public bool shooting = false;
    public float bulletForce = 20f;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void shoot(string ownerTag = "")
    {
        if(!shooting)
        {
            StartCoroutine(Shooting(ownerTag));
        }
    }


    IEnumerator Shooting(string ownerTag = "")
    {
        if (!shooting)
        {
            shooting = true;

            if (anim != null)
            {
                anim.SetBool("Shooting", true);
            }

            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<Bullet>().owner = ownerTag;
            Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
            body.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.2f);

            if (anim != null)
            {
                anim.SetBool("Shooting", false);
            }

            yield return new WaitForSeconds(0.1f);

            shooting = false;
        }
    }
}
