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
    public GameObject caster;

    public GameObject spellCircle;
    private GameObject temp;
    private float circleDestroyTime = 0.5f;


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

        //Comment out if needed
        Destroy(this.gameObject, spellToCast.Lifetime); //automatically destroy the spell after a period of time


        //get the secondary effect //could potentially make an array of secondary structures i guess
        bonus = GetComponent<SecondaryStructure>();

        

        //Set our starting position value so we can compare it later
        startingPosition = transform.position;

        //Instantiate the spell circle asset
        if (temp != null)
        {
            Destroy(temp); //If we havent auto destroyed it in time just destroy the previous circle
        }
        temp = Instantiate(spellCircle, transform.position, transform.rotation); //Create a spell circle at the cast point
        Destroy(temp, circleDestroyTime); //Destroy the magic circle after a period of time
    }

    private void Update()
    {

        //Bolt is a standard projectile, the rest should be raycasts and raycast varients 
        if (spellToCast.spellAttackType != SpellScriptableObject.attackTypes.bolt)
        {
            rayCheck();
        }


        //if you do not want to use the spell range and want just the time to live method for automatic deletion comment this out
        destoryAfterDistance();





    }

    private void destoryAfterDistance()
    {

        //Compare the current position of the spell to its permitted max range

        if (Vector3.Distance(transform.position, startingPosition) > spellToCast.spellRange)
        {
            //Debug.Log(Vector3.Distance(transform.position, startingPosition));
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //APPLY SPELL EFFECTS

        if(other.tag == "Enemy")
        {
            collidedWith = other.gameObject;
            //Damage the enemy
            //other.GetComponent<EnemyManager>().health -= spellToCast.damage;
            other.GetComponent<EnemyManager>().takeDamageFromSpell(spellToCast);

            //Set the object we just hit to be our most recently hit enemy
            caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = collidedWith;


            //Apply any bonus effects
            if (bonus != null) //if the spell has a bonus effect script attached to it apply its affects
            {
                bonus.applyBonusEffect(); 
               
            }

            //We have collided with an enemy so destroy the projectile
            Destroy(this.gameObject);
        }
        
    }
    //May be best to refactor the damaging portion of the spell into its own function
    public void rayCheck()
    {
        //Check if the spell is using the line attack type and perform test as needed
        if(spellToCast.spellAttackType == SpellScriptableObject.attackTypes.line)
        {
            RaycastHit other;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), spellToCast.spellRange, 0))
            {
                Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); //Ray is an infinite line but that doesnt matter as we've tested to see if its within the spells ray range already
                Physics.Raycast(ray, out other); //out keyword allows us to save data on the hit object for us to use
                collidedWith = other.transform.gameObject; //get the object we have hit with our raycast

                //Damage the enemy
                collidedWith.GetComponent<EnemyManager>().takeDamageFromSpell(spellToCast);

                //Set the object we just hit to be our most recently hit enemy for any spells that need it
                caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = collidedWith;

                //Take damage
                if (bonus != null) //if the spell has a bonus effect script attached to it apply its affects
                {
                    bonus.applyBonusEffect();
                }

            }
        }
        else if( spellToCast.spellAttackType == SpellScriptableObject.attackTypes.cube)
        {
            //if (Physics.CheckBox()) { }
        }


        //if(Physics.)
    }
}
