using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime :  SecondaryStructure
{
    public float maxDuration;
    public float currentDuration;
    public float dotDamage;



    private void Awake()
    {
        //Get the spell script we are currently attached to
        spellToApplyTo = GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.start == true)
        {
            applyBonusEffect();
            Debug.Log(start);
        }
    }

    public override void applyBonusEffect()
    {
        //maybe this should call the game manager to handle the dot effect onto the enemy as our projectile will be destoryed on contact?

        //maybe this should call a function in the enemy controller?
        //spellToApplyTo.collidedWith.GetComponent<EnemyManager>().triggerDOT(dotDamage, maxDuration);

        //wont work as projectile will be destroyed
/*        if (currentDuration < maxDuration)
        {
            spellToApplyTo.collidedWith.GetComponent<EnemyManager>().health -= dotDamage;
            currentDuration += currentDuration * Time.deltaTime;

        }*/

        
    }

    private void OnTriggerEnter(Collider other)
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
