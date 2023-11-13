using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private AudioSource hitSound;

    public void mark()
    {
        hitSound.PlayOneShot(hitSound.clip);
        animator.SetTrigger("enable");
    }
}
