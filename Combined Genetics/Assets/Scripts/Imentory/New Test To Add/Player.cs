using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int PlayerMaxDegrees;
    public TextMeshProUGUI tempAnoucementText;
    public GameObject tempObject;
    public GameObject player;
    [SerializeField] private GameObject cam;
    public static GameObject playerForSave;
    [SerializeField] private int playerLevel;
    public KeyCode UpgradeMenuKey;
    public GameObject UpgradeMenuUi;
    [SerializeField] private MouseLook mouseLook;

    [Header("Building")]
    public KeyCode buildingKey;
    public GameObject buildingUi;
    [SerializeField] private Buildingsystem bs;

    [Header("Inventory")]
    public InventoryObject inventory;
    public GameObject invUi;
    public Camera fpsCam;
    public TextMeshProUGUI pickupItemText;
    public GameObject pickupObjects;
    public int range = 3;
    public KeyCode invKey;
    Movement movement;

    void Update()
    {
        if (Input.GetKeyDown(invKey))
        {
            ToggleInv();

            if(invUi.activeSelf == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(UpgradeMenuKey))
        {
            ToggleUpgradeMenu();

            if (UpgradeMenuUi.activeSelf == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(buildingKey))
        {
            ToggleBuilding();

            if (buildingUi.activeSelf == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                bs.enabled = false;
                movement.enabled = false;
                mouseLook.enabled = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                bs.enabled = true;
                movement.enabled = true;
                mouseLook.enabled = true;
            }
        }

        //    RaycastHit hit;
        //    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        //    {
        //          var invItem = hit.collider.GetComponent<ItemTest>();
        //        if (invItem)
        //        {
        //            pickupObjects.SetActive(true);
        //            pickupItemText.text = "Press E to pick up";

        //        }

        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            inventory.AddItem(invItem.item, 1);
        //            Destroy(invItem.gameObject);
        //        }

        //    }

        movement = player.GetComponent<Movement>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var area = other.GetComponent<AreaTempetureScript>();

        if (area)
        {
            if (area.areaTempeture > PlayerMaxDegrees)
            {
                tempObject.SetActive(true);
                tempAnoucementText.text = "You are hot! Get in a cooler area";
            }
        }
    }

    private void ToggleBuilding()
    {
        bool currentState = buildingUi.activeSelf;
        buildingUi.SetActive(!currentState);
    }

    private void ToggleInv()
    {
        bool currentState = invUi.activeSelf;
        invUi.SetActive(!currentState);
    }

    private void ToggleUpgradeMenu()
    {
        bool currentState = UpgradeMenuUi.activeSelf;
        UpgradeMenuUi.SetActive(!currentState);
    }

    private void OnApplicationQuit()
    {
        
    }
}
