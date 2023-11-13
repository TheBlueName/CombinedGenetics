using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    public float recoilX;
    public float recoilY;
    public float recoilZ;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Update()
    {   
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, 3 * Time.deltaTime);
         currentRotation = Vector3.Slerp(currentRotation, targetRotation, 7 * Time.fixedDeltaTime);
          transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilShoot()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
