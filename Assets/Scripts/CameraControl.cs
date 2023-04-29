using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 defaultCameraPos = new (1.5f, 30f, -75f);
    [SerializeField] private float fovMin = 5;
    [SerializeField] private float fovMax = 50;

    private int currentTurn;    //should check whose turn it is
    private float targetFOV = 50;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = gm.getCurrentPlayer();
        resetCamera();
    }

    // Update is called once per frame
    void Update()
    {

        //resets the camera to a neutral position after each turn
        if(currentTurn != gm.getCurrentPlayer())
        {
            resetCamera();
            currentTurn = gm.getCurrentPlayer();
        }

        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Tab)) resetCamera();
        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;


        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 40f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.E)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = -1f;

        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);

        if(Input.mouseScrollDelta.y > 0)
        {
            targetFOV -= 5;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            targetFOV += 5;
        }

        targetFOV = Mathf.Clamp(targetFOV, fovMin, fovMax);

        float zoomSpeed = 10f;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    //resets the camera to a neutral position
    void resetCamera()
    {
        transform.position = defaultCameraPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        targetFOV = fovMax;
        virtualCamera.m_Lens.FieldOfView = targetFOV;
    }
}
