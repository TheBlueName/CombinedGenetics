using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunmods : MonoBehaviour
{
    [SerializeField] private ProjectileGun gunScript;

    //bools
    [Header("Has Things")]
    public bool hasFlashLight;
    public bool hasScope;
    public bool hasGrip;
    public bool hasExtendedMag;

    //gameObjects
    [Header("Objects for decoration")]
    [SerializeField] private GameObject flashLightObject;
    [SerializeField] private GameObject scopeObject;
    [SerializeField] private GameObject gripObject;
    [SerializeField] private GameObject extendMagObject;

    [Header("Values")]
    public float oldRec;
    public float newRec;
    [Space]
    
    public int extendedMagAmmo;
    public int normalMagAmmo;
    //fixes
    private bool allowfov;
    private float ofov;

    void Start()
    {
        ofov = gunScript.adsFov;
    }

    void Update()
    {
        //value null
        if (flashLightObject != null)
        {
            if (!hasFlashLight) flashLightObject.SetActive(false);
            if (hasFlashLight) flashLightObject.SetActive(true);
        }

        if (scopeObject != null)
        {
            if (!hasScope)
            {
                scopeObject.SetActive(false);
                gunScript.adsFov = ofov;
                allowfov = true;
            } 
            if (hasScope)
            {
                scopeObject.SetActive(true);
                fix();
            } 
        }

        if (gripObject != null)
        {
            if (!hasGrip)
            {
                gunScript.Xrecoil = oldRec;
                gripObject.SetActive(false);
            }

            if (hasGrip)
            {
                gunScript.Xrecoil = newRec;
                gripObject.SetActive(true);
            }

            if (hasGrip && Input.GetKeyDown(KeyCode.Mouse1)) gunScript.Xrecoil = gunScript.ADSrecoil + 0.5f;
            if (hasGrip && Input.GetKeyDown(KeyCode.Mouse1)) gunScript.Xrecoil = newRec;
        }

        if (extendMagObject != null)
        {
            if (!hasExtendedMag)
            {
                extendMagObject.SetActive(false);
                gunScript.magazineSize = normalMagAmmo;
            }
            if (hasExtendedMag)
            {
                extendMagObject.SetActive(true);
                gunScript.magazineSize = extendedMagAmmo;
            }
        }

    }

    void fix()
    {
        if(allowfov)
        {
            gunScript.adsFov -= 5;
            allowfov = false;
        }
    }
}
