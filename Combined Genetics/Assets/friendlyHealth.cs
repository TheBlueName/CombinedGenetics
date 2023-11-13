using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendlyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 150f;

    [Header("Sounds")]
    [SerializeField] private AudioSource Sound1;
    [SerializeField] private AudioSource Sound2;
    [SerializeField] private AudioSource Sound3;
    bool canScream;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0) Die();
        
    }

    public void TakeFriendlyDamage()
    {   
            int Chance = Random.Range(0, 3);
             if(Chance == 1) Sound1.Play();
              if(Chance == 2) Sound2.Play();
               if(Chance == 3) Sound3.Play();
    }

    public void TakeEnemyDamage(float damage)
    {
        currentHealth =- damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
