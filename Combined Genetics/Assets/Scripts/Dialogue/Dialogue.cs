using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
    public AudioSource[] sentencesVoiced;
    public string name;
    public bool givesQuest;

    
}
