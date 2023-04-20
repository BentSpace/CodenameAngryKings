using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    float maxHeatlh = 100f;
    float currentHeatlh;
    public float damageFactor = 0.05f; // more or less the object's "defense"
    // Start is called before the first frame update
    void Start()
    {
        currentHeatlh = maxHeatlh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        float mass = collision.rigidbody.mass;
        float velocity = collision.relativeVelocity.magnitude;
        float damage = mass * velocity * damageFactor;
        DamageUnit(damage);
    }

    void DamageUnit(float amount)
    {
        currentHeatlh -= amount;
    }

    public float getCurrentHealth()
    {
        return currentHeatlh;
    }

    public float getCurrentPercent()
    {
        return currentHeatlh / maxHeatlh;
    }
}
