using UnityEngine;

public class InactiveOnButtonPress : MonoBehaviour
{
    [SerializeField] private KeyCode code;

    private DialogueManager manager;

    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(code))
        {
            gameObject.SetActive(false);

                 Cursor.lockState = CursorLockMode.Locked;
                 manager.EndDialogue();
        }
    }
}
