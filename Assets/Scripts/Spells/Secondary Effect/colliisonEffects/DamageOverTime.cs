using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime :  MonoBehaviour
{
    public float maxDuration;
    public float currentDuration;
    public float dotDamage;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            GameObject affectedObject = other.gameObject;

            //Give the component to remove the spell effect after a period of time as projectile will dissapear the responsibility will be on the enemy object to do so
            if (affectedObject.GetComponent<RemoveDotEffect>() == null)
            {
                RemoveDotEffect rDot = affectedObject.AddComponent<RemoveDotEffect>();
            }
            affectedObject.GetComponent<RemoveDotEffect>().maxDuration = maxDuration;
            affectedObject.GetComponent<RemoveDotEffect>().dotDamage = dotDamage;
        }


    }

}
