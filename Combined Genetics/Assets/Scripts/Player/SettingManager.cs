using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingManager : MonoBehaviour
{
    toggleMotionBlur toggleMb;
    public static bool isMotionBlurOn;
    private Volume volume;
    private MotionBlur motionBlur;

    void Update()
    {
        if (toggleMb.motionBlurToggle.isOn) isMotionBlurOn = true;
        else isMotionBlurOn = false;

        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        motionBlur = volume.GetComponent<MotionBlur>();

        if (isMotionBlurOn) motionBlur.active = true;
        else motionBlur.active = true;
    }
}
