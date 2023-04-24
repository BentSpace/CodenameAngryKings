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

    // Update is called once per frame
    void Update()
    {
        int selectIndex = currentSelection.value;
        if (active)
        {
            int currPlayer = gm.getCurrentPlayer() -1;
            if (Input.GetMouseButtonDown(0) && Players[currPlayer].getValue() - buildingValues[selectIndex] >= 0)
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 newPosition = hit.point + hit.normal * (buildingHeights[selectIndex] / 2 + 0.01f);

                    // Add the grid snapping code here
                    float gridSize = 1.0f; // Change this value to modify the grid size
                    newPosition.x = Mathf.Round(newPosition.x / gridSize) * gridSize;
                    newPosition.y = Mathf.Round(newPosition.y / gridSize) * gridSize;
                    newPosition.z = Mathf.Round(newPosition.z / gridSize) * gridSize;

                    Instantiate(buildingPrefabs[selectIndex], newPosition, Quaternion.identity);
                    Players[currPlayer].setValue(Players[currPlayer].getValue() - buildingValues[selectIndex]);
                }
            }

            pointText.text = Players[currPlayer].getValue().ToString();
        }
    }
    public void setActive(bool value)
    {
        active = value;
    }
}
