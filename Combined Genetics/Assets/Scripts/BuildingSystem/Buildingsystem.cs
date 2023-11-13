using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildingsystem : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentobject;
    public Vector3 currentpos;
    public Vector3 currentrot;
    public Transform currentpreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;
    public float offset = 1.0f;
    public float gridSize = 1.0f;
    public bool IsBuilding;
    public MCFace dir;

    private void Update()
    {
        if (IsBuilding)
            StartPreview();
        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown("0") || Input.GetKeyDown("1"))
            SwitchCurrentBuild();

    }

    private void Start()
    {
        ChangeCurrentBuilding(0);
    }

    public void ChangeCurrentBuilding(int cur)
    {
        currentobject = objects[cur];
        if (currentpreview != null)
            Destroy(currentpreview.gameObject);
        GameObject curprev = Instantiate(currentobject.preview, currentpos, Quaternion.Euler(currentrot)) as GameObject;
        currentpreview = curprev.transform;
    }

    public void SwitchCurrentBuild()
    {
        for (int i = 0; i < 2; i ++)
        {
            if (Input.GetKeyDown("" + 1))
                ChangeCurrentBuilding(i);
        }
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
        if (currentobject.sort == Objectsorts.floor)
        {
            dir = GetHitFace(hit2); 
            if (dir == MCFace.Up || dir == MCFace.Down)
            {
                currentpos = hit2.point;
            }
            else
            {
                if (dir == MCFace.North)
                    currentpos = hit2.point + new Vector3(0, 0, 2);

                if (dir == MCFace.South)
                    currentpos = hit2.point + new Vector3(0, 0, -2);

                if (dir == MCFace.East)
                    currentpos = hit2.point + new Vector3(2, 0, 0);

                if (dir == MCFace.West)
                    currentpos = hit2.point + new Vector3(-2, 0, 0);
            }
        }
        else
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

    public void Build()
    {
        PreviewObject PO = currentpreview.GetComponent<PreviewObject>();
        if (PO.isBuildable)
        {
            Instantiate(currentobject.prefab, currentpos, Quaternion.Euler(currentrot));
        }
    }

    public static MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;

        if (incomingVec == new Vector3(0, -1, -1))
            return MCFace.South;

        if (incomingVec == new Vector3(0, -1, 1))
            return MCFace.North;

        if (incomingVec == new Vector3(0, 0, 0))
            return MCFace.Up;

        if (incomingVec == new Vector3(1, 1, 1))
            return MCFace.Down;

        if (incomingVec == new Vector3(-1, -1, 0))
            return MCFace.West;

        if (incomingVec == new Vector3(1, -1, 0))
            return MCFace.East;

        return MCFace.None;
    }
}

[System.Serializable]
public class buildObjects
{
   public string name;
   public GameObject prefab;
   public GameObject preview;
   public Objectsorts sort;
   public int gold;
}

public enum MCFace
{
    None,
    Up,
    Down,
    East,
    West,
    North,
    South
}

