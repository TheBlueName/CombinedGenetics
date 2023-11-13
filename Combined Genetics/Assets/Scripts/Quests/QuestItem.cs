using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private QuestTracker tracker;
    private QuestManager manager;
    [HideInInspector] public bool possible;
    public int itemId;

    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
        tracker = FindObjectOfType<QuestTracker>();
    }

    void Update()
    {
        if(manager.questActive) possible = true;
        if(!manager.questActive) possible = false;
    }

    public void Collect()
    {
        if(tracker.Idadd == itemId)
        {
        tracker.AddGather();
        Destroy(gameObject);
        }
    }
}
