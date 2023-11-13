using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool isBuildable;

    public Transform graphics;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
            col.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
            col.Remove(other);
    }

    void Update()
    {
            ChangeColor();
    }

    public void ChangeColor()
    {
            if (col.Count == 0)
                isBuildable = true;
            else
                isBuildable = false;

            if (col.Count == 0)
                isBuildable = true;
            else
                isBuildable = false;

        if (isBuildable)
        {
            foreach (Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        else
        {
            foreach (Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
    }
}

public enum Objectsorts
{
    normal,
    foundation,
    floor
}