using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Upgrade", menuName = "Upgrade System/HealthUg")]
public class HealthUG : Upgrades
{
    public int AddToHealth;

    public void Awake()
    {
        type = UpgradeType.HealthUG;
    }
}
