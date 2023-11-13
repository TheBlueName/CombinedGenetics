using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
    public GameObject UnderWaterStuff;
    public GameObject GunNStuff;

    private void OnTriggerEnter(Collider other)
    {
        Movement movement = other.GetComponent<Movement>();
        WaterSwim waterswim = other.GetComponent<WaterSwim>();

        if (other.CompareTag("Water"))
        {
            UnderWaterStuff.SetActive(true);
            GunNStuff.SetActive(false);

            if(movement != null) movement.enabled = false;
            if(waterswim != null ) waterswim.enabled = true;
        }

        if (other.CompareTag("Surface"))
        {
            UnderWaterStuff.SetActive(false);
            GunNStuff.SetActive(true);

            if(movement != null) movement.enabled = true;
            if(waterswim != null ) waterswim.enabled = false;
        }
    }
}
