using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingSystemTry : MonoBehaviour
{
    [SerializeField] private int ObjectIndex;
    [SerializeField] private Buildingsystem buildingsystem;
    public void ChangeCurrentBuilding()
    {
        buildingsystem.currentobject = buildingsystem.objects[ObjectIndex];
        if (buildingsystem.currentpreview != null)
            Destroy(buildingsystem.currentpreview.gameObject);
        GameObject curprev = Instantiate(buildingsystem.currentobject.preview, buildingsystem.currentpos, Quaternion.Euler(buildingsystem.currentrot)) as GameObject;
        buildingsystem.currentpreview = curprev.transform;
    }
}
