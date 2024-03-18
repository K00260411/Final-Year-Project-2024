using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerGravity : MonoBehaviour 
{
    public float duration;
    public float forceAmount;
    private GameObject affectedObject;
    private void Awake()
    {
        //if values have not been provided give them a default value
        if(duration == 0)
        {
            duration = 10f;
        }
        if(forceAmount == 0)
        {
            forceAmount= 10f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        affectedObject = other.gameObject;
        //Give the enemy a constant force component to make it fly
        if (affectedObject.GetComponent<ConstantForce>() == null) //&& other.tag == "Enemy")
        {
            ConstantForce cf = affectedObject.AddComponent<ConstantForce>();
        }


        //Give the component its needed details //maybe make that 10f its own variable for customisation?
        affectedObject.GetComponent<ConstantForce>().force = new Vector3(0, forceAmount, 0);

        //Give the component to remove the spell effect after a period of time as projectile will dissapear the responsibility will be on the enemy object to do so
        if (affectedObject.GetComponent<RemoveLowerGravity>() == null)
        {
            RemoveLowerGravity rlg = affectedObject.AddComponent<RemoveLowerGravity>();
        }
        //set the duration the remove script will wait before removing the ConstantForce script
        affectedObject.GetComponent<RemoveLowerGravity>().duration = duration;

    }
}
