using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private float maxHealth = 100f;

    private HealthBar healthBar;

    public GameObject healthBarPrefab;

    private GameObject canvas;

    public AudioClip dieSound;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");

        Vector3 healthbarPos = new Vector3(transform.position.x, transform.position.y, 0);

        GameObject HB = Instantiate(healthBarPrefab, healthbarPos, new Quaternion(), canvas.transform);

        healthBar = HB.GetComponent<HealthBar>();
        healthBar.NPC = gameObject.transform;

        maxHealth = health;
        
        if(healthBar != null)
        {
            healthBar.SetHealth(health, health);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            switch(gameObject.tag)
            {
                case "Player":
                    GameController.instance.AddScore(-1000);
                    GameController.instance.PlayerDie();
                    break;
                case "Enemy":
                    GameController.instance.AddScore(100);
                    break;

            }

            AudioSource.PlayClipAtPoint(dieSound, Vector3.zero);

            Destroy(gameObject);
        }

        if(gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Enemy>().GotHit();
        }

        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        if (healthBar == null) return;

        healthBar.SetHealth(health, maxHealth);
    }
}
