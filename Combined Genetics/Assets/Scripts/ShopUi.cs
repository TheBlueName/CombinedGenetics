using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUi : MonoBehaviour
{
    public GameObject shopUi;
    Movement movement;
    public GameObject player;
    public GameObject MainCamera;
    MouseLook mouseLook;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shopUi.SetActive(true);
            movement.enabled = false;
            mouseLook.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        movement = player.GetComponent<Movement>();
        mouseLook = MainCamera.GetComponent<MouseLook>();
    }
}

