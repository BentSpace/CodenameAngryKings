using UnityEngine;

public class MoveCatapultStop : MonoBehaviour
{
    public float moveIncrement = 0.1f; // Set the value of movement increment
    public float minLocalX = -1f; // Set the minimum allowed local x position
    public float maxLocalX = 2f; // Set the maximum allowed local x position

    bool active = false;
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveAlongLocalX(moveIncrement);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveAlongLocalX(-moveIncrement);
            }
        }
    }

    void MoveAlongLocalX(float increment)
    {
        Vector3 localPosition = transform.parent.InverseTransformPoint(transform.position);
        localPosition.x += increment;
        localPosition.x = Mathf.Clamp(localPosition.x, minLocalX, maxLocalX);
        transform.position = transform.parent.TransformPoint(localPosition);
    }

    public void setActive(bool value)
    {
        active = value;
    }
}
