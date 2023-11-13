using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    HealthUG,
    StrengthUG,
    SpeedUG
}

public abstract class Upgrades : ScriptableObject
{
    public UpgradeType type;
    [TextArea(15, 20)]
    public string description;

}

[System.Serializable]
public class Upgrade
{
    public string Name;
}
