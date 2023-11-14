using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    [Header("Pause Menu")]
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    public bool IsInSettingsMenu;
    MouseLook mouseLook;
    public GameObject mainCam;
    public GameObject settingsMenuUi;

    [Header("Inventory")]
    public InventoryObject inventory;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            if (IsInSettingsMenu)
            {
                settingsMenuUi.SetActive(false);
                IsInSettingsMenu = false;
                GameIsPaused = true;
            }
        }

        mouseLook = mainCam.GetComponent<MouseLook>();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        inventory.Save();
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        mouseLook.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        mouseLook.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetBool()
    {
        IsInSettingsMenu = true;
        GameIsPaused = false;
    }

}
