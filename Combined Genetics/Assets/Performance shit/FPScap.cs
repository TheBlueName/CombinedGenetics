using UnityEngine;

public class FPScap : MonoBehaviour
{
    public int targetRate = 60;

    void Start()
    {
        Application.targetFrameRate = targetRate;
    }
}
