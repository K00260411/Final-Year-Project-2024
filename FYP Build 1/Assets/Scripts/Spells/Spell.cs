using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    
    //take the data of the spell from the spell scriptable object
    public SpellScriptableObject spellToCast;
    //Take in the bonus effect script
    public SecondaryStructure bonus; //make an array of bonus effects in case a spell has multiple?

    //private SphereCollider spellSphere;
    private Collider spellSphere;
    private Rigidbody spellRigid;

    //The object our projectile collides with
    [HideInInspector] //Get rid of variable clutter on Unity's UI
    public GameObject collidedWith = null;

    //I 

    //get the position in the world when our spell is cast, this will be compared to the max permitted range
    public Vector3 startingPosition;


    private void Awake()
    {
        //give the spells sphere its size and make it trigger only to prevent it being solid
        //spellSphere = GetComponent<SphereCollider>();
        spellSphere = GetComponent<Collider>(); //switch back to SphereCollider
        spellSphere.isTrigger = true;
        //spellSphere.radius = spellToCast.spellRadius;
        //spellSphere.bounds.

        //give the spell its rigidbody
        spellRigid = GetComponent<Rigidbody>();
        spellRigid.isKinematic = true;

        Destroy(this.gameObject, spellToCast.Lifetime); //automatically destroy the spell after a period of time

        //get the secondary effect
        bonus = GetComponent<SecondaryStructure>();
    }

    private void Update()
    {

        //This gets 
/*       if(spellToCast.spellAttackType == SpellScriptableObject.attackTypes.line)
        {
            rayCheck();
        }*/

        //Compare the current position of the spell to its permitted max range

        //If exceeds, destroy the object regardless of its TTL remaining
    }

    private void OnTriggerEnter(Collider other)
    {
        //APPLY SPELL EFFECTS

        if(other.tag == "Enemy")
        {
            //Debug.Log("Hit enemy");
            collidedWith = other.gameObject;
            //public GameObject enemy = other.
            //Damage the enemy
            //other.GetComponent<EnemyManager>().health -= spellToCast.damage;
            other.GetComponent<EnemyManager>().takeDamageFromSpell(spellToCast);



            //Apply any bonus effects
            if (bonus != null) //if the spell has a bonus effect script attached to it apply its affects
            {
                bonus.applyBonusEffect(); 
                //bonus.start = true;
               
            }

            //need to find some way to let DOT effects finish

            //We have collided with an enemy so destroy the projectile
            Destroy(this.gameObject);
        }
        
    }

    public void rayCheck()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), spellToCast.spellRange, 0))
        {
            //We have hit an object
            //collidedWith = RaycastHit.

            //Take damage
            if (bonus != null) //if the spell has a bonus effect script attached to it apply its affects
            {
                bonus.applyBonusEffect();
            }

        }
    }
}
