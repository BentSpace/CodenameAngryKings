using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatapult : MonoBehaviour
{
    Rigidbody catapultRigidbody;
    public float catapultForce = 500000f;
    public float reloadForce = 10000f;
    public bool fired = false;
    public bool empty = true;
    public bool active = true;
    public bool testing = false;
 
    public GameObject rockPrefab;
    public GameObject dicePrefab;

    public GameObject reloadLocation;
    private CameraFollowProjectile cameraFollowScript;
    public GameObject newProjectileObject;
    
    public float minCatapultForce = 500000f;
    public float maxCatapultForce = 2000000f;
    public float forceIncrement = 100000f;

    private void Awake()
    {
        cameraFollowScript = Camera.main.GetComponent<CameraFollowProjectile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody>();
        if (empty)
        {
            ReloadCatapult();
        }
    }

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.Space) && active && !empty)
        {
            Fire();
        }
        
        // Handle force modification inputs
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
            {
                IncreaseForce();
            }
            else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.Underscore))
            {
                DecreaseForce();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fired)
        {
            catapultRigidbody.AddForce(transform.up * catapultForce);
        }

        if (!fired && empty)
        {
            catapultRigidbody.AddForce(-transform.up * reloadForce);
        }
    }

    public void Fire()
    {
        fired = true;
        cameraFollowScript.FollowProjectile();
        // Set the instantiated object's Transform as the projectile in the CameraFollowProjectile script
        cameraFollowScript.SetProjectile(newProjectileObject.transform);
    }

    public void setActive(bool value)
    {
        active = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Stop")
        {
            ResetCatapult();
        }

        if (collision.gameObject.name == "ReloadTrigger" && empty)
        {
            ReloadCatapult();
        }
    }
    
    private void ResetCatapult()
    {
        fired = false;
        empty = true;
    }

    private void ReloadCatapult()
    {
        newProjectileObject = Instantiate(dicePrefab, reloadLocation.transform.position, Quaternion.identity);
        empty = false;
       
        
    }
    public void IncreaseForce()
    {
        catapultForce = Mathf.Min(maxCatapultForce, catapultForce + forceIncrement); // Increase force by the increment, up to the maximum force limit
    }

    public void DecreaseForce()
    {
        catapultForce = Mathf.Max(minCatapultForce, catapultForce - forceIncrement); // Decrease force by the increment, down to the minimum force limit
    }
}
