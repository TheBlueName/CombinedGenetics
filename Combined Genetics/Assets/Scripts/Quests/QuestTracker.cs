using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    public int amount;
    public int currentAmount;
    [Space]
    [HideInInspector] public int Idadd;
    public bool gatherQuest;
    private QuestManager manager;
    [Space]
    public TextMeshProUGUI objectiveText;
    [SerializeField] private Animator animator;
    [HideInInspector] public string tt;
    [HideInInspector] public Transform turninTrans;

    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
        objectiveText.text = "";
    }

    void Update()
    {
        if(currentAmount >= amount && manager.questActive)
        {
            objectiveText.text = "Done! Turn in" + amount + " / " + amount;
            Finished();
        }

        //objective text
        if(manager.questActive) objectiveText.text = tt + " " + currentAmount +  " / " + amount;
    }

    public void AddKill()
    {
        currentAmount++;
    }

    public void AddGather()
    {
        currentAmount++;
    }

    public void Finished()
    {
        animator.SetTrigger("Appear");
        Invoke("Restore", 3f);
    }

    void Restore()
    {
        objectiveText.text = "";
    }
}
