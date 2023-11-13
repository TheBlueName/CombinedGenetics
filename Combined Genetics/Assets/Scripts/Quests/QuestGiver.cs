using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    [Header("Attach this to a character with dialogue")]
    [Space]
    [SerializeField] private GameObject questPanel;
    [Space]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    public static bool questSlotFull;

    [Header("Quest Attributes")]
    public string questName;
    public string questDesc;

    public enum QuestType
    {
        Kill, Gather
    }
    public QuestType questType;
    public int requiredAmount;

    public int requiredID;
    
    private Transform turnTrans;
    private QuestTracker tracker;

    [Space]
    [SerializeField] private string objectiveText;

    void Start()
    {
        tracker = FindObjectOfType<QuestTracker>();
    }
    
    public void GiveQuest()
    {
        title.text = questName;
        desc.text = questDesc;

        //important stuff
        if(!questSlotFull)
        {

        questSlotFull = true;

        questPanel.SetActive(true);
         tracker.amount = requiredAmount;

          if(questType == QuestType.Gather) tracker.gatherQuest = true;
          if(questType == QuestType.Kill)
          {
            tracker.gatherQuest = false;
            tracker.Idadd = requiredID;
          } 
            tracker.tt = objectiveText;
            tracker.turninTrans = turnTrans;
        }
        else return;
    }

}
