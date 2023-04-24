using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject rock;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - rock.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = rock.transform.position + offset;
    }
}
