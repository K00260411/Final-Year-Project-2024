using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageCaster : SecondaryStructure
{
    public float damageToSelf;
    

    public override void applyBonusEffect()
    {
        //Check what kind of entity cast the spell and heal them from their required script
        if (caster.tag == "Player")
        {
            caster.GetComponent<PlayerManager>().health -= damageToSelf;
        }
        else if(caster.tag == "Enemy")
        {
            caster.GetComponent<EnemyManager>().health -= damageToSelf;
        }
        else
        {
            //replace with any more if statements for other entities as needed
        }
    }

    private void Awake()
    {
        caster = GetComponent<Spell>().caster;
        applyBonusEffect(); //called in awake to ensure the effect is applied once regardless of the spell it is attached to 
    }
}
