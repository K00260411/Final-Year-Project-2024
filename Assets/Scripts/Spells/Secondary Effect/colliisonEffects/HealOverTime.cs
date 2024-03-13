using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : MonoBehaviour
{
    public float maxDuration;
    public float currentDuration;
    public float hotHealing;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject affectedObject = other.gameObject;

            //Give the component to remove the spell effect after a period of time as projectile will dissapear the responsibility will be on the enemy object to do so
            if (affectedObject.GetComponent<RemoveHealing>() == null)
            {
                RemoveHealing rDot = affectedObject.AddComponent<RemoveHealing>();
            }
            affectedObject.GetComponent<RemoveHealing>().maxDuration = maxDuration;
            affectedObject.GetComponent<RemoveHealing>().hotHealing = hotHealing;
        }


    }

}
