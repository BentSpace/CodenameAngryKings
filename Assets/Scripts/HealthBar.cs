using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image remainingHP;
    Image differenceHP;
    [SerializeField]
    HealthSystem health;

    float previousHealth;
    // Start is called before the first frame update
    void Start()
    {
        remainingHP = transform.Find("Remaining").GetComponent<Image>();
        differenceHP = transform.Find("Difference").GetComponent<Image>();
        previousHealth = health.getCurrentHealth();
        if(!health)
        {
            health = transform.parent.GetComponent<HealthSystem> ();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health.getCurrentHealth() != previousHealth)
        {
            remainingHP.fillAmount = health.getCurrentPercent();
            StartCoroutine(drainDifference());
        }
        previousHealth = health.getCurrentHealth();
    }

    IEnumerator drainDifference()
    {
        yield return new WaitForSeconds(0.8f);
        while(differenceHP.fillAmount > remainingHP.fillAmount + 0.015f)
        {
            differenceHP.fillAmount = Mathf.Lerp(differenceHP.fillAmount, remainingHP.fillAmount, 0.5f);
            yield return null;
        }
        differenceHP.fillAmount = remainingHP.fillAmount;
        yield return null;
    }
}
