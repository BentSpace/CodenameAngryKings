using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class CameraControlNew : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] private CinemachineVirtualCamera defaultVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera followVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera[] catapultVirtualCameras;
    [SerializeField] private Vector3 defaultCameraPos = new Vector3(0f, 30f, -125f);
    [SerializeField] private float fovMin = 5;
    [SerializeField] private float fovMax = 50;

    private int currentTurn;    //should check whose turn it is
    private float targetFOV = 50;
    public Transform diceTransform;
    public float followTime = 5f; // Time to follow the dice

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
        followVirtualCamera.Priority = 0;
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
        FollowDiceForSeconds();
    }

    // Switch to the given player's catapult virtual camera
    void SwitchToCatapultCamera(int playerIndex)
    {
        defaultVirtualCamera.Priority = 0;
        followVirtualCamera.Priority = 0;
        catapultVirtualCameras[playerIndex].Priority = 10;
    }

    // Switch to the dice virtual camera, wait for a certain number of seconds, then switch back to the default camera
    

    public void FollowDiceForSeconds()
    {
        StartCoroutine(FollowDiceCoroutine());
    }

    private IEnumerator FollowDiceCoroutine()
    {
        if (diceTransform == null)
        {
            yield break; // Exit if the dice transform hasn't been set
        }

        // Set the target of the follow virtual camera to the dice
        followVirtualCamera.Follow = diceTransform;
        followVirtualCamera.Priority = 20;

        yield return new WaitForSeconds(followTime); // Wait for the specified number of seconds

        // Switch back to the default view
        followVirtualCamera.Priority = 10;
        SwitchToDefaultCamera();
    }

}
