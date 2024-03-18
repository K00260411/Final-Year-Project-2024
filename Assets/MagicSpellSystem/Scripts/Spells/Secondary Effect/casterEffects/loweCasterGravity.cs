using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loweCasterGravity : SecondaryStructure
{
    public float duration;
    public float forceAmount;


    private void Awake()
    {
        //Get the object that has cast the spell
        caster = GetComponent<Spell>().caster;
        applyBonusEffect();
    }

    public override void applyBonusEffect()
    {
        //Add the required component and apply the force
        caster.AddComponent<ConstantForce>();
        caster.GetComponent<ConstantForce>().force = new Vector3(0, forceAmount, 0);

        //Add the remove component and set its duration
        if (caster.GetComponent<RemoveLowerGravity>() == null)
        {
            RemoveLowerGravity rlg = caster.AddComponent<RemoveLowerGravity>();
        }
        caster.GetComponent<RemoveLowerGravity>().duration = duration;
    }

}
