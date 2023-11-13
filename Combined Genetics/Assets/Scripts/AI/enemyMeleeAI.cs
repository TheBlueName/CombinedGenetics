using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMeleeAI : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField] private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    
    [Header("Enemy Stats")]
    [SerializeField] bool Neutral = false;
    
    //Attacking
    [Header("Attacking")]
    [SerializeField] private Collider MeleeWeapon;
    [SerializeField] private Animator weaponAnim;

    public int EnemyDamage = 10;
    public float HitDuration = 2.0f;

    [Header("FleeShit")]
    [SerializeField] private Transform FleePoint;

    //Patroling
    [Header("Patrol Options")]


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    [Header("States")]
    public float sightRange;
    public float sightRangeAdd = 15f;

    public float AttackRange;

    public bool playerInSightRange;
    public bool InWeaponRange;
    bool focusingOnSomething;

    [Header("Agent Things")]
    
    //So you can set it to 0, just put the speed in here
    [SerializeField] private float ChaseSpeed = 5f;
    [SerializeField] private float IdleSpeed = 3f;
    

    [Header("Colors (I was really bored)")]

    //I was really bored so you can customize the colors of the spheres
    [SerializeField] private Color SightRangeColor;
    [SerializeField] private Color AttackRangeColor;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        MeleeWeapon.enabled = false;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);       
         InWeaponRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);


            //set the enemy target transform
            Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange + 2);

            foreach (Collider nearbyObject in colliders)
            {
                Indicator ind = nearbyObject.GetComponent<Indicator>();
                if(ind != null)
                {
                    player = ind.gameObject.GetComponent<Transform>();
                }
                
            }

        if (!playerInSightRange) Patroling();
        if (playerInSightRange && !Neutral) ChasePlayer();



        if (InWeaponRange)
        { 
            AttackPlayer();
            MeleeWeapon.enabled = true;
            agent.isStopped = true;
            weaponAnim.SetBool("Walking", false);
        }

        if (!InWeaponRange)
        {
             MeleeWeapon.enabled = false;
             agent.isStopped = false;
        }
    }


    private void Patroling()
    {
        agent.speed = IdleSpeed;
        
        if (!walkPointSet) SearchWalkPoint();
        weaponAnim.SetBool("Walking", false);

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }


    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    

    private void ChasePlayer()
    {
        if(player != null) agent.SetDestination(player.position);
        
        weaponAnim.SetBool("Walking", true);

        agent.speed = ChaseSpeed;
    }

    public void Flee()
    {
        agent.SetDestination(FleePoint.position);

        Debug.Log("Received Signal");
    }

    void AttackPlayer()
    {
        weaponAnim.SetTrigger("Attack");
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(HitDuration);
    }

    public void Hit()
    {
        sightRange += 15;
        if(Neutral) ChasePlayer();

        int flinchChance = Random.Range(0, 10);
        if(flinchChance > 7) weaponAnim.SetTrigger("Flinching");
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = SightRangeColor;

        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = AttackRangeColor;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
