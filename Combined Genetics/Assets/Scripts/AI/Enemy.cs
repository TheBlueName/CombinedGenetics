using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyHP = 50;
    public int ID;
    [Space]

    [SerializeField] private float FleeHP;
    [SerializeField] private GameObject Ragdollmodel;
    [SerializeField] private UnityEngine.AI.NavMeshAgent navMesh;

    [Header("Audio")]
    [SerializeField] private AudioSource hurt1;
    [SerializeField] private AudioSource hurt2;
    [SerializeField] private AudioSource hurt3;
    private hit killmarker;

    private QuestManager manager;
    private QuestTracker tracker;

    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
        tracker = FindObjectOfType<QuestTracker>();
        ID = 0;
        killmarker = GameObject.Find("Killmarker").GetComponent<hit>();
    }

    void Update()
    {
        
        if(EnemyHP < FleeHP)
        {
            //maybe fleeing
            float Chance = Random.Range(1.0f, 10.0f);
            if(Chance > 8.0f)
            {
                 if(navMesh != null) BroadcastMessage("Flee");
                 Debug.Log("Fleeing");
            }
        }

        // Die function
        if (EnemyHP <= 0)
        {
            if(manager.questActive && !tracker.gatherQuest && ID == tracker.Idadd) tracker.AddKill();

            //die bitch
            if(navMesh != null) navMesh.isStopped = false;
            if(killmarker) killmarker.mark();
            Destroy(gameObject);
        }
    }

    // Enemy takes damage function
    public void TakeDamage(int Damage)
    {
        //more game juice bc you can never have enough
        int audioChance = Random.Range(0, 4);

         if(audioChance == 1 && hurt1 != null) hurt1.PlayOneShot(hurt1.clip);
         if(audioChance == 2 && hurt2 != null) hurt2.PlayOneShot(hurt2.clip);
         if(audioChance == 3 && hurt3 != null) hurt3.PlayOneShot(hurt3.clip);



        //make sure this shit works
        EnemyHP -= Damage;
        
        //detect if hit then set the sight range higher
        if(navMesh != null) BroadcastMessage("Hit");
    }

}
