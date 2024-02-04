using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerGravity : SecondaryStructure
{
    public float duration;
    public float forceAmount;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Give the enemy a constant force component to make it fly
        if (other.gameObject.GetComponent<ConstantForce>() == null) //&& other.tag == "Enemy")
        {
            other.gameObject.AddComponent<ConstantForce>();
        }

        //Give the component its needed details //maybe make that 10f its own variable for customisation?
        other.gameObject.GetComponent<ConstantForce>().force = new Vector3(0, forceAmount, 0);

        //Give the component to remove the spell effect after a period of time as projectile will dissapear the responsibility will be on the enemy object to do so
        if (other.gameObject.GetComponent<RemoveLowerGravity>() == null)
        {
            other.gameObject.AddComponent<RemoveLowerGravity>();
        }
        //apply any needed details to the spells duration
        other.gameObject.GetComponent<RemoveLowerGravity>().duration = duration;

    }
}
