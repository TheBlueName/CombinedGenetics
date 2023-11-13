using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;
    public bool shouldLoadOnAwake;

    private void Awake()
    {
        if (shouldLoadOnAwake) SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerEnter(Collider LS)
    {
        if(LS.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
