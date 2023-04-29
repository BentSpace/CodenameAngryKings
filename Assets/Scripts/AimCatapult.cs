using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCatapult : MonoBehaviour
{
    public float rotationSpeed = 30f; // Degrees per second
    public float rotationAmount = 5f; // Degrees of rotation per key press

    public bool active = false;

    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(RotateCatapult(-1)); // Rotate left
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(RotateCatapult(1)); // Rotate right
            }
        }
    }

    IEnumerator RotateCatapult(int direction)
    {
        float rotationRemaining = rotationAmount;

        while (rotationRemaining > 0)
        {
            float rotationThisFrame = Mathf.Min(rotationSpeed * Time.deltaTime, rotationRemaining);
            transform.Rotate(Vector3.up, direction * rotationThisFrame, Space.World);
            rotationRemaining -= rotationThisFrame;
            yield return null;
        }
    }

    public void setActive(bool value)
    {
        active = value;
    }
}


