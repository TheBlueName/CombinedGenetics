using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactWeaponHolder : MonoBehaviour
{
    public Animator anim;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            anim.SetBool("IsInContact", true);
        }

        if (other.CompareTag("Shop"))
        {
            anim.SetBool("IsInContact", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            anim.SetBool("IsInContact", false);
        }

        if (other.CompareTag("Shop"))
        {
            anim.SetBool("IsInContact", false);
        }
    }
}
