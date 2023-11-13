using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryObject inventory;
    public GameObject settingsMenuUi;
    public Toggle motionBlurToggle;
    Player player;
    private CamShake shake;

    void Start()
    {
        shake = GameObject.Find("Main Camera").GetComponent<CamShake>();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        inventory.Load();

        player.LoadPlayer();
    }

    void Awake()
    {
    }

    public void SetBool()
    {
        if (!motionBlurToggle.isOn) motionBlurToggle.isOn = true;
        else motionBlurToggle.isOn = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
