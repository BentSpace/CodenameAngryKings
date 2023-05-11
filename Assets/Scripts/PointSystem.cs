using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    int points = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setValue(int val)
    {
        points = val;
    }

    public int getValue()
    {
        return points;
    }
    
    public void DecreasePoints(int value)
    {
        Debug.Log("Decrease points");
        points -= value;

        // Check to make sure points doesn't fall below 0
        if (points < 0)
        {
            points = 0;
        }
    }

}
