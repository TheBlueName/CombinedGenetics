using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeSceneController : MonoBehaviour
{
    private GameObject[] bgMusicObj;
    private AudioSource bgMusic;
    public bool IsMainMenu;


    // Start is called before the first frame update
    void Start()
    {
        if (IsMainMenu) VolumeSlider.volInt = 1;
        else VolumeSlider.volInt = bgMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        bgMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        bgMusic.volume = VolumeSlider.volInt;
    }

    public void UpdateInPause()
    {
        bgMusic.volume = VolumeSlider.volInt;
    }
}
