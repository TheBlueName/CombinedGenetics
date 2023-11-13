using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    public GameObject itemPrefab;
    public Transform itemSpawn;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(itemPrefab, itemSpawn.position, Quaternion.identity);
            Destroy(gameObject);
        }

        healthbar.SetHealth(currentHealth);
    }
}
