using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public Transform NPC;
    public Vector3 offset;


    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetHealth(float health, float maxHealth)
    {
        gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
    }

    void FixedUpdate()
    {
        if (NPC == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(NPC.position + offset);
            slider.transform.position = pos;
        }
    }
}
