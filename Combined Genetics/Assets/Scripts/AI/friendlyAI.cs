using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class friendlyAI : MonoBehaviour
{
    [Header("Basic Stats")]

    public float sightRange = 15f;
    public float attackRange = 10f;
    [SerializeField] private float stoppingRadius = 0.75f;
    public float normalMoveSpeed = 3f;

    [Space]
    [SerializeField] private Transform firePoint;
    [Space]
    [SerializeField] private float timeBetweenAttacks;

    [Space]

    public bool isMelee = false;
    public bool followsPlayer;
    public bool moves = true;

    [Header("AI things")]

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform head;
    private int missCounter;

    [Space]

    public LayerMask groundLayer, enemyLayer;
    
    //bools & shit
    bool enemyInSightRange;
    bool enemyInAttackRange;
    bool alreadyAttacked;
    bool gonnaAttack;


    [Header("Patrol")]
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float chillMovementSpeed = 1.5f;
    [Space]

    private Vector3 currentEnemyTrans;
    private Transform playerTrans;
    private float same;

    [Header("Graphics & Such")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shootSound;
    [Space]
    [SerializeField] private Animator animator;

    [Header("Weapon Stats")]
    public int damage = 10;
    public float range = 50;
    [Space]
    private int magAmmo;
    public int maxAmmo = 12;
    public float reloadTime = 1f;

    void Awake()
    {
        playerTrans = GameObject.Find("Player").transform;
        same = stoppingRadius;
        magAmmo = maxAmmo;
    }

    void Update()
    {
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyLayer);
         enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyLayer);

            

            //set the enemy target transform
            Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);

            foreach (Collider nearbyObject in colliders)
            {
                Enemy enemy = nearbyObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    Transform tempTrans = enemy.gameObject.GetComponent<Transform>();
                    currentEnemyTrans = tempTrans.position;
                }
                
            }


        //bools for checking states
        if(!enemyInSightRange) Chill();
         if(enemyInSightRange) Chase();

        if(enemyInAttackRange) Attack();

        //check if the anything is in range and then apply the corresponding functions
        if(!enemyInAttackRange && enemyInSightRange) Chase();
         if(!enemyInAttackRange && !enemyInSightRange) Chill();

        if(!enemyInAttackRange && !enemyInSightRange && followsPlayer) FollowPlayer();

        //reloading
        if(magAmmo <= 0)
        {
            Invoke("Reload", 0.1f);
        }
    }

    //patrol shit
    void Chill()
    {
        agent.isStopped = false;
        transform.LookAt(null);
        
        animator.SetBool("Attacking", false);
        if(moves) agent.isStopped = false;
        if(walkPointRange > 0) animator.SetBool("Walking", true);
        
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    //more patrolling
        private void SearchWalkPoint()
        {
            animator.SetBool("Moving", false);
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
                walkPointSet = true;
        }

    void Chase()
    {   
        
        agent.isStopped = false;
        transform.LookAt(null);

        //animations
        animator.SetBool("Moving", true);
        animator.SetBool("Attacking", false);
        if(walkPointRange > 0) animator.SetBool("Walking", false);

        //dont do some shit
        if(moves) agent.speed = normalMoveSpeed;


        if(moves) agent.SetDestination(currentEnemyTrans);
    }

    void FollowPlayer()
    {
        if(moves) agent.speed = chillMovementSpeed;

        agent.SetDestination(playerTrans.position);
    }

    private void Attack()
    {
        transform.LookAt(new Vector3(currentEnemyTrans.x, transform.position.y, currentEnemyTrans.z));
        head.LookAt(currentEnemyTrans);

        animator.SetBool("Attacking", true);

        agent.isStopped = true;

        if (!alreadyAttacked)
        {

            //decoration here
            muzzleFlash.Play();

            shootSound.pitch = Random.Range(0.75f, 1.0f);
            shootSound.PlayOneShot(shootSound.clip);

            ///Attack code here
            RaycastHit hit;
            if(Physics.Raycast(firePoint.position, firePoint.transform.forward, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if(enemy != null) enemy.TakeDamage(damage);
                if(enemy = null) missCounter++;
            }
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }


    //draw gizmos for visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //more reloading
    private void Reload()
    {
        alreadyAttacked = true;
        animator.SetBool("Reloading", true);
        Invoke("ReloadDone", reloadTime);
    }

    private void ReloadDone()
    {
        alreadyAttacked = false;
        animator.SetBool("Reloading", false);
    }
}
