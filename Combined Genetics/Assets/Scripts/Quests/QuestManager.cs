using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [HideInInspector] public bool questActive;
    [SerializeField] private QuestTracker tracker;

    public void AcceptQuest()
    {

        questActive = true;
        questPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FinishQuest()
    {
        questActive = false;
        Debug.Log("Quest Finished");
        BroadcastMessage("Finished");
    }

}
