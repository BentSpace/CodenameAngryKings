using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatapult : MonoBehaviour
{
    public GameObject hinge;

    public float fireSpeed = 90;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(hinge.transform.position, Vector3.forward, fireSpeed * Time.deltaTime);
    }
    
}
