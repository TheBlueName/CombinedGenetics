using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    //auto door
    private Transform player;
    public float openRange = 5f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (distanceToPlayer.magnitude <= openRange) Open();
        if (distanceToPlayer.magnitude > openRange) Close();
    }

    public void Open()
    {
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
    }
}
