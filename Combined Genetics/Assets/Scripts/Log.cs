using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    NewInventory imentory;
    public GameObject player;
    public int woodGiveAmount;
    public float itemDuration = 150f;

    void Start()
    {
        
    }

    void Update()
    {
        imentory = player.GetComponent<NewInventory>();
        StartCoroutine(DestroyGameobjectAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            imentory.AddLogs(woodGiveAmount);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyGameobjectAfterTime()
    {
        yield return new WaitForSeconds(itemDuration);
        Destroy(gameObject);
    }
}
