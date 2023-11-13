using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    [Space]
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private GameObject weaponHolder;
    private Queue<string> sentences;
    private Queue<AudioSource> sentencesVoiced;

    public Animator animator;
    public GameObject act;

    [Header("Quest")]
    [HideInInspector] public bool gquest;
    public QuestGiver giver;
    public Collider pickUpColl;
    private MouseLook look;
    bool currentState;


    void Start()
    {
        sentences = new Queue<string>();
        sentencesVoiced = new Queue<AudioSource>();
        animator = GameObject.Find("WeaponHolder").GetComponent<Animator>();
        look = GameObject.Find("Main Camera").GetComponent<MouseLook>();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        currentState = dialogueBox.activeSelf;
        dialogueBox.SetActive(!currentState);

        Cursor.lockState = CursorLockMode.None;
        nameText.text = dialogue.name;
        act.SetActive(false);
        if(look != null) look.enabled = false;

        sentences.Clear();
        sentencesVoiced.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (AudioSource audioSource in dialogue.sentencesVoiced)
        {
            sentencesVoiced.Enqueue(audioSource);
        }

        DisplayNextSentence();
    }

    void OnTriggerEnter(Collider other)
    {
        var giv = pickUpColl.GetComponent<QuestGiver>();
        {
            if(giv != null) giver = giv;
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        if (sentencesVoiced.Count > 0)
        {
            AudioSource audioSource = sentencesVoiced.Dequeue();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public void EndDialogue ()
    {
        currentState = dialogueBox.activeSelf;
        dialogueBox.SetActive(!currentState);

        Debug.Log ("End Of Conversation");
        animator.SetBool("Low", false);
        act.SetActive(true);
        if(look != null) look.enabled = true;
        if(gquest) giver.GiveQuest();

        if(!gquest) Cursor.lockState = CursorLockMode.Locked;
    }
}
