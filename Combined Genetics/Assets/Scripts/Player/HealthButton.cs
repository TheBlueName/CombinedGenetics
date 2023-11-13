using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthButton : MonoBehaviour
{
    public HealthUG healthUG;
    public int healthIfUnlocked = 110;
    public Color unlockedColor;
    public GameObject button;
    private bool HasUnlocked;
    public GameObject buttonsThatGetUnlocked;
    public Color LockedColor;
    private void Update()
    {
        if (HasUnlocked)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Image>().color = unlockedColor;
            buttonsThatGetUnlocked.GetComponent<Image>().color = LockedColor;
        }

        if (!HasUnlocked)
        {
            buttonsThatGetUnlocked.GetComponent<Image>().color = unlockedColor;
        }
    }
    public void AddAmountToHealth()
    {
        if (!HasUnlocked)
        {
            HealthScript.MaxHealth += healthUG.AddToHealth;
            HasUnlocked = true;
        }
    }
}
