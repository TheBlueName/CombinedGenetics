using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicChange : MonoBehaviour
{
    public AudioSource ambientMusic;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            ambientMusic.PlayOneShot(ambientMusic.clip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ambientMusic.Stop();
    }
}
