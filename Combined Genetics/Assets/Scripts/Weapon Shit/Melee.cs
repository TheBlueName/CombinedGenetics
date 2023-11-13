using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int weaponDamage = 10;
    public float timeBetweenAttacks = 1.0f;
    public float clickAttackDifference = 0.25f;
    public float weaponRange = 2f;
    bool canAttack = true;

    [Space]

    private GameObject mainCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource swingSound;
    
    //game juice
    private hit hitmarker;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private AudioSource hitSound;

    private Enemy enemy;
    private Transform trans;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        hitmarker = GameObject.Find("Hitmarker").GetComponent<hit>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canAttack) Swing();
    }

    void Swing()
    {
        canAttack = false;

        animator.SetTrigger("Attacking");

        //sound
        swingSound.pitch = Random.Range(1.0f, 0.8f);
        swingSound.PlayOneShot(swingSound.clip);
        
        StartCoroutine(HitPause());

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, weaponRange))
        {
            enemy = hit.collider.GetComponent<Enemy>();
            if(enemy)
            {
                Invoke("Attack", clickAttackDifference);
                trans = hit.transform;
            }
        }
    }

    IEnumerator HitPause()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    void Attack()
    {
            enemy.TakeDamage(weaponDamage);
                
            //da juice
            hitmarker.mark();
            Instantiate(bloodParticle, trans.position, Quaternion.identity);

            hitSound.pitch = Random.Range(1.0f, 0.8f);
            hitSound.Play();
    }
}
