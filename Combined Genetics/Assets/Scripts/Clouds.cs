using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    void CreateWorleyPointsBuffer(int numCellsPerAxis, string bufferName)
    {
        var points = new Vector3[numCellsPerAxis * numCellsPerAxis * numCellsPerAxis];
        float cellSize = 1f / numCellsPerAxis;

        for (int x = 0; x < numCellsPerAxis; x++)
        {
            for (int y = 0; y < numCellsPerAxis; y++)
            {
                for (int z = 0; z < numCellsPerAxis; z++)
                {
                    var randomOffset = new Vector3(Random.value, Random.value, Random.value);
                    var position = (new Vector3(x, y, z) + randomOffset) * cellSize;
                    int index = x + numCellsPerAxis * (y + z * numCellsPerAxis);
                    points[index] = position;
                }
            }
        }
        CreateBuffer (points, sizeof(float) * 3, bufferName);
    }

    //Create buffer with some data, and set in shader. Also add to list of buffers to be released
    void CreateBuffer(System.Array data, int stride, string bufferName, int kernel = 0)
    {
        var buffer = new ComputeBuffer (data.Length, stride, ComputeBufferType.Raw);
        //bufferToRelease.Add (buffer);
        buffer.SetData (data);
        //noiseCompute.SetBuffer (kernel, bufferName, buffer);
    }
}
