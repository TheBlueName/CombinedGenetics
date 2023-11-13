using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public AudioSource aud;
    public GameObject obj;
    [Space]
    public Animator anim;
    public string animName;

    [Header("What To Do")]
    public bool destroysItself;
    [Space]
    public bool playsSound;
    public bool setsObjectActive;
    public bool playsAnimation;

    //what this script will do is if you touch a trigger collider you do something according to the bools
    
    void OnTriggerEnter(Collider doSomething)
    {
        if(doSomething.CompareTag("Player"))
        {
            if(playsSound) aud.Play();
            if(setsObjectActive) obj.SetActive(true);
            if(playsAnimation) anim.Play(animName);

            if(destroysItself) Destroy(gameObject);
        }
    }
}
