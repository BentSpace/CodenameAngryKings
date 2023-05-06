using UnityEngine;
using System.Collections;

public class CameraFollowProjectile : MonoBehaviour
{
    public Transform projectile; // The projectile to follow
    public float followDuration = 5.0f; // Duration to follow the projectile (in seconds)
    private bool isFollowing; // Whether the camera is currently following the projectile
    public Vector3 offset; // The initial offset between the camera and the projectile
    private float timer; // Timer to keep track of the follow duration
    private Vector3 originalPosition;

    void Start()
    {
        offset = transform.position - projectile.position;
        timer = 0.0f;
        isFollowing = false;
    }

    public void FollowProjectile()
    {
        // Store the original position of the camera
        originalPosition = transform.position;
        isFollowing = true;
        timer = 0.0f;
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = projectile.position + offset;

            // Update the timer and stop following the projectile when the duration has elapsed
            timer += Time.deltaTime;
            if (timer >= followDuration)
            {
                isFollowing = false;
                // Reset the camera's position to the original position
                transform.position = originalPosition;
            }
        }
    }
    
    public void SetProjectile(Transform newProjectile)
    {
        projectile = newProjectile;
    }
}

