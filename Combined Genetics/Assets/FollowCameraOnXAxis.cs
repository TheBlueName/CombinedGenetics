using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraOnXAxis : MonoBehaviour
{
    [SerializeField] private Transform camObj;
    [SerializeField] private Transform playerObj;

    void Update()
    {
        transform.LookAt(new Vector3(transform.position.x, camObj.position.y, transform.position.z));
    }
}
