using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallDestruct : MonoBehaviour
{
    [SerializeField] private Transform pfWallBroken;
    public int WallHealth = 30;

    public void WallDamage(int Damage)
    {
        WallHealth -= Damage;
    }

    private void Update()
    {
        if (WallHealth == 0)
        {
            Destroy(gameObject);
            Instantiate(pfWallBroken, transform.position, transform.rotation);
        }
    }
}