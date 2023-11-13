using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 10;
    public float PushForce = 5f;

    [SerializeField] private GameObject BloodParticle;
    [SerializeField] private GameObject sparkEffect;

    private GameObject hitmarker;

    public bool IsAllies = false;


    [Header("Penetrating enemies")]
    //ricochet shit
    public bool multipleHits = false;
    [SerializeField] private int hitAmount = 2;
    private int currentAmount;


    private void Start()
    {   
        if(!IsAllies) hitmarker = GameObject.Find("Hitmarker");
        StartCoroutine(Performance());
    }


    // Damage enemy trigger
    void OnTriggerEnter(Collider DmgEnemy)
    {
        {
            Enemy enemy = DmgEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Enemy takes damage
                enemy.TakeDamage(Damage);
                 if(!IsAllies) hitmarker.GetComponent<hit>().mark();

                Instantiate(BloodParticle, transform.position, transform.rotation);
                 if(!multipleHits) K();
                  if(multipleHits) currentAmount++;
            }

            //i used a tag go cry there was no other way
            if(DmgEnemy.CompareTag("Ground"))
            {
                Instantiate(sparkEffect, transform.position, transform.rotation);
                K();
            }

            
            //wall destrucion
            wallDestruct desctruct = DmgEnemy.GetComponent<wallDestruct>();
            if(desctruct != null) desctruct.WallDamage(Damage);

            if(DmgEnemy.CompareTag("nodestroy"))
            {
                return;
            }

            //add force when shooting
            Rigidbody rb = DmgEnemy.GetComponent<Rigidbody>();
            if(rb != null)
            { 
                rb.AddForce(transform.forward * PushForce);
                K();
            }

        }

    }

    // If the bullet goes in the sky, it'll get destroyed
    IEnumerator Performance()
    {
        yield return new WaitForSeconds(5);
        K();
    }

    void K()
    {
        Destroy(gameObject);
    }
}
