using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public bool givesAQuest = false;
    [HideInInspector] public bool alreadyTalkedTo = false;
   
    void Start()
    {
    }
    
    public void StartConversation()
    {
        if(!alreadyTalkedTo)
        {      
             Cursor.visible = true;
              FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
               if(givesAQuest) FindObjectOfType<DialogueManager>().gquest = true;
                GameObject.Find("WeaponHolder").GetComponent<Animator>().SetBool("Low", true);
                alreadyTalkedTo = true;      

        }
    }

    private void Update()
    {
        if(givesAQuest) alreadyTalkedTo = false;
    }

}
