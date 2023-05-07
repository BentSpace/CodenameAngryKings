using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundEffects : MonoBehaviour
{

    public AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundEffect.Play();
    }
}
