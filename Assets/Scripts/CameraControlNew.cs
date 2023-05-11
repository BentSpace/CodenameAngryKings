using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class CameraControlNew : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] private CinemachineVirtualCamera defaultVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera diceVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera[] catapultVirtualCameras;
    [SerializeField] private Vector3 defaultCameraPos = new Vector3(0f, 30f, -125f);
    [SerializeField] private float fovMin = 5;
    [SerializeField] private float fovMax = 50;

    private int currentTurn;    //should check whose turn it is
    private float targetFOV = 50;

    // Start is called before the first frame update.
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
        defaultVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(defaultVirtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        
    }

    //resets the camera to a neutral position
    void resetCamera()
    {
        transform.position = defaultCameraPos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        targetFOV = fovMax;
        defaultVirtualCamera.m_Lens.FieldOfView = targetFOV;
        SwitchToDefaultCamera();
    }

    // Switch to the default virtual camera
    void SwitchToDefaultCamera()
    {
        defaultVirtualCamera.Priority = 10;
        diceVirtualCamera.Priority = 0;
        foreach (var camera in catapultVirtualCameras)
        {
            camera.Priority = 0;
        }
    }
    
    public void OnAttackAndConfirmButtonPressed()
    {
        SwitchToCatapultCamera(currentTurn - 1);
    }

    public void OnFireButtonPressed()
    {
        FollowDiceForSeconds(5);
    }

    // Switch to the given player's catapult virtual camera
    void SwitchToCatapultCamera(int playerIndex)
    {
        defaultVirtualCamera.Priority = 0;
        diceVirtualCamera.Priority = 0;
        catapultVirtualCameras[playerIndex].Priority = 10;
    }

    // Switch to the dice virtual camera, wait for a certain number of seconds, then switch back to the default camera
    async void FollowDiceForSeconds(int seconds)
    {
        defaultVirtualCamera.Priority = 0;
        foreach (var camera in catapultVirtualCameras)
        {
            camera.Priority = 0;
        }
        diceVirtualCamera.Priority = 10;

        await Task.Delay(seconds * 1000);

        SwitchToDefaultCamera();
    }
}
