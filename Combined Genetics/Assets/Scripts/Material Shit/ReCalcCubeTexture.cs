using UnityEngine;

public class ReCalcCubeTexture: MonoBehaviour
{
    public Material targetMaterial;
    public Vector3 tiling = new Vector3(1f, 1f, 1f);

    // Other script code...
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;
    }

    void Update()
    {
        targetMaterial.mainTextureScale = tiling;
    }


}