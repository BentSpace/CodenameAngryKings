using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    GameManager gm;

    [SerializeField]
    GameObject CardPrefab;
    [SerializeField]
    GameObject DominoPrefab;
    [SerializeField]
    GameObject DicePrefab;
    [SerializeField]
    GameObject CatapultPrefab;
    [SerializeField]
    GameObject BallistaPrefab;
    [SerializeField]
    TMP_Dropdown currentSelection;

    [SerializeField]
    PointSystem Player1;
    [SerializeField]
    PointSystem Player2;

    [SerializeField]
    TextMeshProUGUI pointText;

    GameObject[] buildingPrefabs;
    int[] buildingValues;
    float[] buildingHeights; // i donked something up in making the prefabs so i'm hacking together the solution here
    PointSystem[] Players;

    bool active = false;

    public float blockPlacementDistance = 5f;
    
    private GameObject currentObject;
    private float currentRotation = 0f;
    public float rotationSpeed = 10f; // Degrees per frame
    
    private GameObject previewObject;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        buildingValues = new int[5] { 5, 10, 25, 50, 40 };
        buildingPrefabs = new GameObject[5] { CardPrefab, DominoPrefab, DicePrefab, CatapultPrefab, BallistaPrefab };
        buildingHeights = new float[5] {0.7f, 1.0f, 0.99f, 1.0f, 1.3f};
        Players = new PointSystem[2] { Player1, Player2 };
        mainCamera = Camera.main;
    }
    
    private void SetCollidersEnabled(GameObject obj, bool enabled)
    {
        foreach (Collider collider in obj.GetComponentsInChildren<Collider>())
        {
            collider.enabled = enabled;
        }
    }


    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    void Update()
    {
        int selectIndex = currentSelection.value;
        int currPlayer = gm.getCurrentPlayer() - 1;
        Vector3 newPosition = Vector3.zero;

        if (active)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Preview")))
            {
                newPosition = hit.point + hit.normal * (buildingHeights[selectIndex] / 2 + 0.01f);

                // grid snapping
                float gridSize = 1.0f;
                newPosition.x = Mathf.Round(newPosition.x / gridSize) * gridSize;
                newPosition.y = Mathf.Round(newPosition.y / gridSize) * gridSize;
                newPosition.z = Mathf.Round(newPosition.z / gridSize) * gridSize;

                if (previewObject == null)
                {
                    previewObject = Instantiate(buildingPrefabs[selectIndex], newPosition, Quaternion.identity);
                    SetLayerRecursively(previewObject, LayerMask.NameToLayer("Preview"));
                    // Ignore physics simulation for the preview object
                    Rigidbody rb = previewObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                        SetCollidersEnabled(previewObject, false);
                    }
                }
                else
                {
                    previewObject.transform.position = newPosition;
                }
                
                if (Input.GetMouseButtonDown(0) && Players[currPlayer].getValue() - buildingValues[selectIndex] >= 0)
                {
                    // Instantiate the actual object and destroy the preview
                    GameObject actualObject = Instantiate(buildingPrefabs[selectIndex], newPosition, Quaternion.identity);
                    Destroy(previewObject);
                    // Reset the layer of the actual object
                    SetLayerRecursively(actualObject, 0); // assuming 0 is the default layer
                    
                    // Subtract the cost of the building from the current player's points
                    Players[currPlayer].DecreasePoints(buildingValues[selectIndex]);
                }
            }
        }
        else
        {
            // Destroy the preview object if the building system is deactivated
            if (previewObject != null)
            {
                Destroy(previewObject);
            }
        }
    }






    public void setActive(bool value)
    {
        active = value;
    }

    public void resetPoints()
    {
        Players[gm.getCurrentPlayer() - 1].setValue(100);
    }
}
