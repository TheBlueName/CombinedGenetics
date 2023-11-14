using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public GameObject flashLightLight;
    [SerializeField] private AudioSource toggleSound;
    //random pitch that you can select
    [SerializeField] private float lowestPitch, highestPitch;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        bool currentState = flashLightLight.activeSelf;
        flashLightLight.SetActive(!currentState);

        //sound design
        toggleSound.pitch = Random.Range(lowestPitch, highestPitch); //set the pitch to a random between these 2 values
        toggleSound.PlayOneShot(toggleSound.clip); //now play the clip
    }

}
