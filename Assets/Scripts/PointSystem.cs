using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    int value = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setValue(int val)
    {
        value = val;
    }

    public int getValue()
    {
        return value;
    }
}
