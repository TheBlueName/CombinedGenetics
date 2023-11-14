using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopUi;

    public TextMeshProUGUI medkitText;

    public TextMeshProUGUI woodText;

    public GameObject player;

    NewInventory imentory;

    public GameObject MainCamera;

    MouseLook mouseLook;

    Movement movement;

    public bool canMedkitBeBought;

    public bool canSellWood;

    void Update()
    {
        MedkitColor();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shopUi.SetActive(false);
            movement.enabled = true;
            mouseLook.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        mouseLook = MainCamera.GetComponent<MouseLook>();
        movement = player.GetComponent<Movement>();
    }

    public void MedkitColor()
    {
        imentory = player.GetComponent<NewInventory>();

        if (imentory.currencyAmount < 2)
        {
            medkitText.color = Color.red;
            canMedkitBeBought = false;
        }
        
        if (imentory.currencyAmount > 2)
        {
            medkitText.color = Color.green;
            canMedkitBeBought = true;
        }

        if (imentory.currencyAmount < 1)
        {
            woodText.color = Color.red;
            canSellWood = false;
        }

        if (imentory.woodAmount > 0)
        {
            woodText.color = Color.green;
            canSellWood = true;
        }
        if(imentory.woodAmount <= 0)
        {
            canSellWood = false;
            woodText.color = Color.red;
        }
    }

    public void BuyMedkit()
    {
        if (canMedkitBeBought == true)
        {
            imentory.medkitAmount++;
            imentory.currencyAmount -= 3;
            Debug.Log("Bought Medkit!");
        }
    }

    public void SellWood()
    {
        if (canSellWood == true)
        {
            imentory.woodAmount--;
            imentory.currencyAmount += 3;
        }
    }
}
