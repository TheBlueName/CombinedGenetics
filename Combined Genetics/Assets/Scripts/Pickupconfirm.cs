using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickupconfirm : MonoBehaviour
{
    [SerializeField] private GameObject Confirmation;
    [SerializeField] private TMP_Text TextContent;
    bool Active = false;

    [Header("Objects Find")]
    public GameObject player;
    public GameObject MainCamera;
    [Space]
    public GameObject ModsMenu;
    public InventoryObject inventory;
    Movement movement;
    MouseLook mouseLook;
    Outline outlineWb;
    Outline outlineSp;

    private QuestItem itemy;
    private Transform mainCamera;

    void Start()
    {
        Confirmation.SetActive(false);
        mainCamera = GameObject.Find("Main Camera").transform;


        //do some things

        movement = player.GetComponent<Movement>();
        mouseLook = MainCamera.GetComponent<MouseLook>();

    }

    void Update()
    {
        if(Active = true && Input.GetKeyDown(KeyCode.E))
        {
            Confirmation.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Physics.Raycast(mainCamera.position, transform.forward, out hit, 3);

            //pick up weapon
            var puWeapon = hit.transform.GetComponent<PickUpWeapon>();
            if(puWeapon != null && !PickUpWeapon.slotFull) puWeapon.PickUp();
            if(puWeapon = null) return;

            //quest item
            QuestItem qItem = hit.transform.GetComponent<QuestItem>();
            if(qItem != null) qItem.Collect();

            //dialogue
            var tDia = hit.transform.GetComponent<TouchDialogue>();
            if(tDia != null) tDia.StartConversation();


        }

        //ignore this it's bug fixing
        Collider coll = gameObject.GetComponent<Collider>();
        if(Input.GetKeyDown(KeyCode.Mouse1)) coll.enabled = false;
        if(Input.GetKeyUp(KeyCode.Mouse1)) coll.enabled = true;

    }
    
    void OnTriggerEnter(Collider Enter)
    {


        //weapons
        var puw = Enter.GetComponent<PickUpWeapon>();
        if(puw != null && PickUpWeapon.slotFull == false)
        {
            Confirmation.SetActive(true);
            TextContent.text = "Press E to pick up " + puw.name;
        }

        //pick up quest goal
        QuestItem item = Enter.GetComponent<QuestItem>();
        if(item != null)
        {
            Confirmation.SetActive(true);
              TextContent.text = "Press E to pick up " + item.name +  " (OBJECTIVE ITEM)";   
               itemy = item;
        }

        var td = Enter.GetComponent<TouchDialogue>();
        if(td != null && !td.alreadyTalkedTo)
        {
            Confirmation.SetActive(true);
            TextContent.text = "Press E to talk to " + td.name;
        }
    }

    void OnTriggerExit(Collider Exit)
    {
            Active = false;
            Confirmation.SetActive(false);
    }
}

