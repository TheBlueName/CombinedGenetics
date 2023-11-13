using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkbenchUI : MonoBehaviour
{
    public GameObject WorkbenchUi;
    Movement movement;
    public GameObject player;
    public GameObject MainCamera;
    MouseLook mouseLook;
    NewInventory inventoryScript;
    public float craftingTime = 3;
    public AudioSource craftingSound;
    public GameObject craftingIsDoneText;
    public GameObject craftingText;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WorkbenchUi.SetActive(false);
            movement.enabled = true;
            mouseLook.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            craftingIsDoneText.SetActive(false);
        }

        movement = player.GetComponent<Movement>();
        mouseLook = MainCamera.GetComponent<MouseLook>();
        inventoryScript = player.GetComponent<NewInventory>();
        
    }

    public void craftSpear()
    {
        if (inventoryScript.canCraft == true && inventoryScript.spearAmount < 3 && inventoryScript.canCraftNow == true && inventoryScript.woodAmount > 2)
        {
            craftingIsDoneText.SetActive(false);
            craftingText.SetActive(true);
            StartCoroutine(Crafting());
            craftingSound.Play();
        }
    }

    public IEnumerator Crafting()
    {
        inventoryScript.canCraftNow = false;
        yield return new WaitForSeconds(craftingTime);
        inventoryScript.canCraftNow = true;
        inventoryScript.woodAmount -= 3;
        inventoryScript.spearAmount++;
        craftingIsDoneText.SetActive(true);
        craftingText.SetActive(false);
    }
}
