using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunColorCustomize : MonoBehaviour
{
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private Material[] mats;

    public void DropDownSample(int index)
    {
        switch (index)
        {
            case 0: mr.material = mats[0];break;
            case 1: mr.material = mats[1]; break;
        }
    }
}
