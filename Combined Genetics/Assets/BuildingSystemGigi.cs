using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystemGigi : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentobject;
    private Vector3 currentpos;
    private Vector3 currentrot;
    public Transform currentpreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;
    public float offset = 1.0f;
    public float gridSize = 1.0f;
    public bool IsBuilding;

    void Update()
    {
        if (IsBuilding)
            StartPreview();
    }

    public void ChangeCurrentBuilding(int cur)
    {
        currentobject = objects[cur];
        if (currentpreview != null)
            Destroy(currentpreview.gameObject);
        GameObject curprev = Instantiate(currentobject.preview, currentpos, Quaternion.Euler(currentrot)) as GameObject;
        currentpreview = curprev.transform;
    }

    private void Start()
    {
        ChangeCurrentBuilding(0);
    }

    public void StartPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
                ShowPreview(hit);
        }
    }

    public void ShowPreview(RaycastHit hit2)
    {
        currentpos = hit2.point;
        currentpos -= Vector3.one * offset;
        currentpos /= gridSize;
        currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
        currentpos *= gridSize;
        currentpos += Vector3.one * offset;
        currentpreview.position = currentpos;
        if (Input.GetMouseButtonDown(1))
            currentrot += new Vector3(0, 45, 0);
        currentpreview.localEulerAngles = currentrot;
    }

}

[System.Serializable]
public class buildObjectss
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
}
