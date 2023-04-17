using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatapult : MonoBehaviour
{
    // public GameObject hinge;
    // public float fireSpeed = 90;
    Rigidbody catapultRigidbody;
    public float catapultForce = 1000000f;

    // Start is called before the first frame update
    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody>();
        Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        catapultRigidbody.AddForce(transform.up * catapultForce);
        // transform.RotateAround(hinge.transform.position, Vector3.forward, fireSpeed * Time.deltaTime);
    }

    void Fire()
    {
        
    }
}
