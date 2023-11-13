using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    public Text ShowText;
    public string Dialogue;

    void Start()
    {
        ShowText.text = Dialogue;
        
        ShowText.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider TextColl)
    {
        if(TextColl.gameObject.tag == "Player")
        {
        ShowText.gameObject.SetActive(true);
        }
   }

    void OnTriggerExit(Collider TextColl)
    {
        ShowText.gameObject.SetActive(false);
    }
}
