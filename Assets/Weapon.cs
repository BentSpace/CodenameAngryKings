using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int Player; // which player does this weapon belong to?
    Catapult catapult;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Catapult")
        {
            catapult = gameObject.GetComponent<Catapult>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {

        if(gameObject.tag == "Catapult")
        {
            catapult.firescript.Fire();
        }    
    }

    public void setActive(bool value)
    {
        if (gameObject.tag == "Catapult")
        {
            catapult.setActive(value);
        }
    }
}
