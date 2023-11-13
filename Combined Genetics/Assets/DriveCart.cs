using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCart : MonoBehaviour
{
    public Transform cartParent;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            transform.SetParent(cartParent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
