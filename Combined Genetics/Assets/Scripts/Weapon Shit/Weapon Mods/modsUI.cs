using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class modsUI : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] private KeyCode openKey;
    [SerializeField] private GameObject uiObject;
    [Header("Stat Look")]
    public TextMeshProUGUI spreadText;
    public TextMeshProUGUI recoilText;
    public TextMeshProUGUI rateText;

    void Update()
    {
        if(Input.GetKeyDown(openKey)) Toggle();
    }

    public void OpenMenu()
    {
        uiObject.SetActive(true);
    }

    public void CloseMenu()
    {
        uiObject.SetActive(false);
    }

    void Toggle()
    {
        bool currentState = uiObject.activeSelf;
        uiObject.SetActive(!currentState);
    }
}
