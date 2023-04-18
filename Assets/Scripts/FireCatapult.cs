using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatapult : MonoBehaviour
{
    // public GameObject hinge;
    // public float fireSpeed = 90;
    Rigidbody catapultRigidbody;
    public float catapultForce = 1000000f;
    bool fired = false;
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        catapultRigidbody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && active)
        {
            Fire();

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fired)
        {
            catapultRigidbody.AddForce(transform.up * catapultForce);
        }
        // transform.RotateAround(hinge.transform.position, Vector3.forward, fireSpeed * Time.deltaTime);
    }

    void Fire()
    {
        fired = true;
    }

    public void setActive(bool value)
    {
        active = value;
    }
}
