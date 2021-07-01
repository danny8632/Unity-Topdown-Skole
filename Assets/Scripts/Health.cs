using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private float maxHealth = 100f;

    public HealthBar healthBar;

    private void Start()
    {
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
                    break;
                case "Enemy":
                    GameController.instance.AddScore(100);
                    break;

            }

            Destroy(gameObject);
        }

        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        if (healthBar == null) return;

        healthBar.SetHealth(health, maxHealth);
    }
}
