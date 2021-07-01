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
    public float damage = 10f;

    public AudioClip gunSound;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void shoot()
    {

        if(!shooting)
        {
            StartCoroutine(Shooting());
        }
    }


    IEnumerator Shooting()
    {
        if (!shooting)
        {
            shooting = true;

            if (anim != null)
            {
                anim.SetBool("Shooting", true);
            }

            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();

            bulletScript.owner = gameObject.tag;
            bulletScript.damage = damage;

            body.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);

            AudioSource.PlayClipAtPoint(gunSound, Vector3.zero);

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
