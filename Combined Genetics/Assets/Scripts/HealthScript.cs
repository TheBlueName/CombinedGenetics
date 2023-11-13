using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    public static float MaxHealth = 100f;
    public float MaxHealthTest;
    [SerializeField] private float CurrentHealth;
    [SerializeField] private Text HealthText;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private Text OxygenText;
    [SerializeField] private Slider HealthBar;

    [SerializeField] private GameObject hurtBorder;
    [SerializeField] private CamShake shake;

    [Header("Sounds")]
    [SerializeField] private AudioSource HardHurtSound;
    [SerializeField] private AudioSource SoftHurtSound;

    void Start()
    {
        CurrentHealth = MaxHealth;
        DeathScreen.SetActive(false);
        Time.timeScale = 1f;
        OxygenText.enabled = true;
        HealthText.enabled = true;
    }

    public void DamagePlayer(int EnemyDamage)
    {
       CurrentHealth -= EnemyDamage;
       Debug.Log("Damaging Player"); 

       StartCoroutine(shake.Shake(.2f, .05f));

       if(EnemyDamage > 20) HardHurtSound.Play();
       if(EnemyDamage < 20) SoftHurtSound.Play();

       if(CurrentHealth <= 0)
       {
            Die();
            OxygenText.enabled = false;
            HealthText.enabled = false;
            Debug.Log("U ded");
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
       }

    }

    void Update()
    {
        HealthText.text = CurrentHealth.ToString();
        HealthBar.value = CurrentHealth;
        MaxHealthTest = MaxHealth;
    }

    public void Die()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0.1f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        Debug.Log("Respawning");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("YouQuit");
    }
}