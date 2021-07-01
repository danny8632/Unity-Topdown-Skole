using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform NPC;
    public Color low;
    public Color high;
    public Vector3 offset;
    public Image Fill;

    public void SetHealth(float health, float maxHealth)
    {
        Debug.Log(health);
        Debug.Log(maxHealth);


        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        //Fill.color = Color.Lerp(low, high, slider.normalizedValue);
    }

    void FixedUpdate()
    {
        if(NPC == null)
        {
            Destroy(gameObject);
        }

        Vector2 pos = Camera.main.WorldToScreenPoint(NPC.position + offset);
        slider.transform.position = pos;
    }
}
