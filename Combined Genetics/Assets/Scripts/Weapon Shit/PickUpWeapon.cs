using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [Header("Projectile weapon")]
    public ProjectileGun gunScript;

    
    [Header("Melee weapon")]
    public Melee meleeScript;
    public bool isMelee = false;
    
    [Header("Else")]
    public bool equipped;
    public static bool slotFull;
    [Space]
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;
    [SerializeField] private AudioSource pickUpSound;
    [Space]
    public Rigidbody rb;
    public Collider coll;
    private Transform player, gunContainer, fpsCam;
    public Animator animator;
    [Space]
    [SerializeField] private float Xscale = 0.2f;
    [SerializeField] private float Yscale = 0.2f;
    [SerializeField] private float Zscale = 0.2f;
    [Space]
    [SerializeField] private GameObject Arm1;
    [SerializeField] private GameObject Arm2;
    [Space]
    [SerializeField] private LayerMask lm;
    [SerializeField] private LayerMask layernone;

    [Header("Lerping")]
    [SerializeField] private float lerpSpeed = 0.5f;
    private Animator aanimator;
    private float origin;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        gunContainer = GameObject.Find("InputWeapon").GetComponent<Transform>();
        fpsCam = GameObject.Find("Main Camera").GetComponent<Transform>();
        aanimator = GameObject.Find("WeaponHolder").GetComponent<Animator>();
        origin = dropForwardForce;

        
        //Setup
        if (!equipped)
        {
            if(!isMelee) gunScript.enabled = false;
            if(isMelee) meleeScript.enabled = false;
            rb.isKinematic = false;
            coll.enabled = true;
            Arm1.SetActive(false);
            Arm2.SetActive(false);
            animator.enabled = false;
            gameObject.layer = layernone;
            gameObject.tag = "Weapons";
        }
        
        if (equipped)
        {
            if(!isMelee) gunScript.enabled = true;
            if(isMelee) meleeScript.enabled = true;
            rb.isKinematic = true;
            coll.enabled = false;
            slotFull = true;
            gameObject.layer = lm;
            gameObject.tag = "Untagged";
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        if(equipped && Input.GetKey(KeyCode.T))
        {
            aanimator.Play("WeaponHolderThrow");
            dropForwardForce =+ 15;
            Invoke("Drop", 0.5f);
        }

    }

    public void PickUp()
    {
        equipped = true;
        slotFull = true;

        // Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, lerpSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(Xscale, Yscale, Zscale);

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.enabled = false;

        //Enable script
        if(!isMelee) gunScript.enabled = true;
        if(isMelee) meleeScript.enabled = true;

        Arm1.SetActive(true);
        Arm2.SetActive(true);

        animator.enabled = true;

        gameObject.layer = lm;

        gameObject.tag = "Untagged";

        //play the audio source
        pickUpSound.Play();
    }

    private void Drop()
    {
        aanimator.Play("WeaponHolderIdle");
        equipped = false;
        slotFull = false;

        gameObject.layer = layernone;

        animator.enabled = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.enabled = true;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 5);

        //Disable script

            Arm1.SetActive(false);
            Arm2.SetActive(false);
            
        if(!isMelee) gunScript.enabled = false;
        if(isMelee) meleeScript.enabled = false;

        gameObject.tag = "Weapons";
        Invoke("ResetForce", 0.2f);
    }

    void ResetForce()
    {
        dropForwardForce = origin;
    }
}
