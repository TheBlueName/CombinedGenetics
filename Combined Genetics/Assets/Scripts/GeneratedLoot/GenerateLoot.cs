using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLoot : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;

    [SerializeField] private Rigidbody dekselRb;

    [SerializeField] private Transform pos;

    void Update()
    {
        
    }

    public void Generate()
    {      
            dekselRb.AddForce(transform.up * 50);

            int generateint = Random.Range(0, 5);

            if(generateint == 1) Instantiate(Item1, pos.position, pos.rotation);
            if(generateint == 2) Instantiate(Item2, pos.position, pos.rotation);
            if(generateint == 3) Instantiate(Item3, pos.position, pos.rotation);
            if(generateint == 4) Debug.Log("Bro got unlucky");
    }
}
