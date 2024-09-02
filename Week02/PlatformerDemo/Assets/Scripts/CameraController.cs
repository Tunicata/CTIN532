using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
    }


    void PathFinding()
    {
        int cubeLevel = 8;
        int maxLen = cubeLevel - 1;
        bool[,] pathGrid = new bool[cubeLevel, cubeLevel];
        int rowIdx = 0;
        int colIdx = cubeLevel - 1;

        pathGrid[rowIdx, colIdx] = true;

        while (rowIdx < maxLen && colIdx > 0)
        {
            if (Random.Range(0, 1) == 0)
            {
                rowIdx += 1;
            }
            else
            {
                colIdx -= 1;
            }
            
            pathGrid[rowIdx, colIdx] = true;
        }
        
        while (colIdx > 0)
        {
            colIdx -= 1;
            pathGrid[rowIdx, colIdx] = true;
        }

        while (rowIdx < maxLen)
        {
            rowIdx += 1;
            pathGrid[rowIdx, colIdx] = true;
        }
    }
}