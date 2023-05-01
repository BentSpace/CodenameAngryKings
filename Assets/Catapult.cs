using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField]
    public FireCatapult firescript;
    [SerializeField]
    public AimCatapult aimscript;
    [SerializeField]
    public MoveCatapultStop stopscript;

    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            firescript.setActive(true);
            aimscript.setActive(true);
            stopscript.setActive(true);
        }
        else
        {
            firescript.setActive(false);
            aimscript.setActive(false);
            stopscript.setActive(false);
        }
    }
    public void setActive(bool value)
    {
        active = value;
    }
}
