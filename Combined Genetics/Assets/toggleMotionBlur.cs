using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleMotionBlur : MonoBehaviour
{
    public Toggle motionBlurToggle;
    public static bool IsTicked;

    void Update()
    {
        motionBlurToggle.isOn = IsTicked;
    }

    public void SetBool()
    {
        if (!IsTicked) IsTicked = true;
        else IsTicked = false;
    }
}
