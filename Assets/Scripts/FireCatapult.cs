using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatapult : MonoBehaviour
{
    Rigidbody catapultRigidbody;
    public float catapultForce = 1000000f;
    public float reloadForce = 10f;
    public bool fired = false;
    public bool empty = false;
    public bool active = true;
 
    public GameObject rockPrefab;

    public GameObject reloadLocation;
    // Start is called before the first frame update
    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {

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
        Instantiate(rockPrefab, reloadLocation.transform.position, Quaternion.identity);
        empty = false;
    }
}
