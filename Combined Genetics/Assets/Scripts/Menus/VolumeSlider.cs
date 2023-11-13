using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public Slider volSlider;
    public static float volInt;
    public bool IsPauseMenuSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (IsPauseMenuSlider)
        {
            volSlider.value = volInt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        volInt = volSlider.value;
    }

    public void SetBool()
    {
        IsPauseMenuSlider = true;
    }
}
