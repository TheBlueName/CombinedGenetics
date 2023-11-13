using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAndDrink : MonoBehaviour
{
    public int thirstRestore = 3;
    PlayerNeeds playerNeeds;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerNeeds = player.GetComponent<PlayerNeeds>();
    }

    public void DrinkOnButtonPress()
    {
        playerNeeds.Drink();
        Destroy(gameObject);
    }
}
