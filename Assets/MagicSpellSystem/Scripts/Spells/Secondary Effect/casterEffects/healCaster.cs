using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class healCaster : SecondaryStructure
{
    public float healthToHealOnCast;

    public override void applyBonusEffect()
    {
        //Check what kind of entity cast the spell and heal them from their required script
        if (caster.tag == "Player")
        {
            caster.GetComponent<PlayerManager>().health += healthToHealOnCast;
        }
        else if (caster.tag == "Enemy")
        {
            caster.GetComponent<EnemyManager>().health += healthToHealOnCast;
        }
        else
        {
            //replace with any more if statements for other entities as needed

        }
    }

    private void Awake()
    {
        //Get the object that has casted the spell
        caster = GetComponent<Spell>().caster;
        applyBonusEffect(); //called in awake to ensure the effect is applied once regardless of the spell it is attached to 
    }



    
}
