using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float Radius = 5f;
    public float force = 700f;
    public int Damage = 40;
    public AudioSource ExplosionSound;

    public GameObject ExplosionEffect;
    [Header("Type Grenade")]
    public bool ImpactGrenade = false;
    [Space]
    public bool suctionGrenade = false;
    public float explosionDelay = 2f;

    [Header("Camera Shake")]
    [SerializeField] private float duration = 0.75f;
    [SerializeField] private float magnitude = 0.2f;


    float countDown;
    bool HasExploded = false;

    void Start()
    {
        if(!ImpactGrenade) countDown = delay;
        if(ImpactGrenade) countDown = 5;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0 && !HasExploded)
        {
            if(!suctionGrenade) Explode();
            if(suctionGrenade) ReverseExplode();
            HasExploded = true;
        }
        
    }

    void OnTriggerEnter(Collider OnImpact)
    {
        if(OnImpact.CompareTag("Ground"))
        {
        Debug.Log("Grenade Touched Ground");

        if(ImpactGrenade) Explode();
        if(!ImpactGrenade) return;
        
        }
        else
        {
            return;
        }
    }

    void Explode()
    {
       //effect
        Instantiate(ExplosionEffect, transform.position, transform.rotation);

       //Get objects
       Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

       foreach (Collider nearbyObject in colliders)
       {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, Radius);
            }

            wallDestruct wd = nearbyObject.GetComponent<wallDestruct>();

            if(wd != null) wd.WallDamage(Damage);

            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                //juice
                hit hitmarker = GameObject.Find("Hitmarker").GetComponent<hit>();
                if(hitmarker != null) hitmarker.mark();

                //function
                enemy.TakeDamage(Damage);
            }

            CamShake shake = nearbyObject.GetComponent<CamShake>();
            if(shake = null) Debug.Log("Shake not found");
            if (shake != null)
            {
                StartCoroutine(shake.Shake(duration, magnitude));
            }

       }
       //say bye to grenade
       Destroy(gameObject);
    }

    void ReverseExplode()
    {

       //Get objects
       Collider[] colliderse = Physics.OverlapSphere(transform.position, Radius);

       foreach (Collider nearbyObjects in colliderse)
       {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(-force, transform.position, Radius);
            }  
       }

       Invoke("Explode", explosionDelay);

    }


}
