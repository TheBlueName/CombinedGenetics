using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int EnemyDamage = 10;

    [Header("Bools")]
    public bool destroyOnTrigger;

    
    void OnTriggerEnter(Collider DamPlayer)
    {
        HealthScript hs = DamPlayer.GetComponent<HealthScript>();
        if(hs != null)
        {
            Debug.Log("Hit Player");
            hs.DamagePlayer(EnemyDamage);

            if(destroyOnTrigger) Destroy(gameObject);
        }
    }
}
