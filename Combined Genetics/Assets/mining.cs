using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mining : MonoBehaviour
{
    [Header("Inventory Stuff")]
    public InventoryObject inventory;
    [Header("Raycast Stuff")]
    public Camera fpsCam;
    public float range = 5f;
    [Header("Pickaxe Stuff")]
    public Animator anim;
    public int pickaxeDamage;
    [Header("Mineable Object Stuff")]
    public GameObject impactEffect;
    public ObjectLife objLife;
    public GameObject[] healthBar;
    public float TimeUntilDeactivation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Mine();
            anim.SetTrigger("IsMining");
        }
    }

    void Mine()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            var item = hit.collider.GetComponent<ObjectLife>();
            if (item)
            {
                AddDamage(pickaxeDamage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            var stone = hit.collider.CompareTag("Stone");
            if (stone)
            {
                healthBar[0].SetActive(true);
                StartCoroutine(HealthBarDestroy(healthBar[0]));
            }
        }
    }

    void AddDamage(int damage)
    {
        objLife.currentHealth -= damage;
    }

    IEnumerator HealthBarDestroy(GameObject healthbars)
    {
        yield return new WaitForSeconds(TimeUntilDeactivation);
        healthbars.SetActive(false);
    }
}
