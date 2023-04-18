using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject blockPrefab;
    public float blockPlacementDistance = 5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPosition = hit.point + hit.normal * (blockPrefab.transform.localScale.y / 2 + 0.01f);

                // Add the grid snapping code here
                float gridSize = 1.0f; // Change this value to modify the grid size
                newPosition.x = Mathf.Round(newPosition.x / gridSize) * gridSize;
                newPosition.y = Mathf.Round(newPosition.y / gridSize) * gridSize;
                newPosition.z = Mathf.Round(newPosition.z / gridSize) * gridSize;

                Instantiate(blockPrefab, newPosition, Quaternion.identity);
            }
        }
    }
}