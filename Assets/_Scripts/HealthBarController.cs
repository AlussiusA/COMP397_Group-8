using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    public Slider healthBarSlider;

    [Header("Health Values")]
    [Range(0, 100)]
    public int currentHealth = 100;
    [Range(1, 100)]
    public int maximumHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBarSlider.value = currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maximumHealth;
        healthBarSlider.value = currentHealth;
    }

    public void SetHealth(int healthValue)
    {
        currentHealth = healthValue;
        healthBarSlider.value = healthValue;
    }
}
