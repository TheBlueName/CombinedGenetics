using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUniversal : MonoBehaviour
{

    public enum ItemType
    {
        LOG, AMMO, HEALING, FOOD
    }

    public ItemType itemType;
    
    [Header("If healing")]
    public int HealAmount = 25;
    [SerializeField] private AudioSource HealSound;

    [Header("If food")]
    public int Nutrition = 10;

    private Camera playerCam;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 

    }

}
