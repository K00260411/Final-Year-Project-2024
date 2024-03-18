using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//automatically create these components if the user has not provided one to ensure compatibility
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Spell : MonoBehaviour
{
    
    [Header("Scriptable object to get details from")]
    //take the data of the spell from the spell scriptable object
    public SpellScriptableObject spellToCast;
    //private SphereCollider spellSphere;
    private Collider spellSphere; 
    private Rigidbody spellRigid;
    //The object our projectile collides with
    [HideInInspector] //Get rid of variable clutter on Unity's UI
    public GameObject collidedWith = null;
    //get the position in the world when our spell is cast, this will be compared to the max permitted range
    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public GameObject caster;
    //Varables for spawning / despawning spell circle
    public GameObject spellCircle;
    private GameObject temp;
    private float circleDestroyTime = 0.5f;

    private void Awake()
    {
        //give the spells default sphere its size and make it trigger only to prevent it being solid
        spellSphere = GetComponent<SphereCollider>(); 
        spellSphere.isTrigger = true;  
        //give the spell its rigidbody
        spellRigid = GetComponent<Rigidbody>();
        spellRigid.isKinematic = true;

        //Comment out if needed
        Destroy(this.gameObject, spellToCast.Lifetime); //automatically destroy the spell after a period of time

        //Set our starting position value so we can compare it later
        startingPosition = transform.position;

        //Instantiate the spell circle asset if you have one applied
        if (temp != null)
        {
            Destroy(temp); //If we havent auto destroyed it in time just destroy the previous circle
        }
        if(spellCircle != null)
        {
            temp = Instantiate(spellCircle, transform.position, transform.rotation); //Create a spell circle at the cast point
            Destroy(temp, circleDestroyTime); //Destroy the magic circle after a period of time
        }
    }

    private void Update()
    {

        //Bolt is a standard projectile, the rest should be raycasts and raycast varients 
        if (spellToCast.spellAttackType != SpellScriptableObject.attackTypes.bolt)
        {
            //check for all other types
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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //APPLY SPELL DAMAGE

        if(other.gameObject.tag == "Enemy")
        {
            collidedWith = other.gameObject;
            //Damage the enemy
            //other.GetComponent<EnemyManager>().health -= spellToCast.damage;
            other.GetComponent<EnemyManager>().takeDamageFromSpell(spellToCast);

            //Set the object we just hit to be our most recently hit enemy
            caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = collidedWith;

            //We have collided with an enemy so destroy the projectile
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerManager>().health = -spellToCast.damage;
        }

        if(other.gameObject.tag == "Enviroment")
        {
            /* //if we dont have the script to bounce off things destroy the spell
        if(GetComponent<RicochetMovement>() == null)
        {
            Destroy(this.gameObject);
        }*/

            Destroy(this.gameObject);
        }

        
    }




    public void rayCheck()
    {
        //Check if the spell is using the line attack type and perform test as needed
        if(spellToCast.spellAttackType == SpellScriptableObject.attackTypes.line)
        {
            RaycastHit other;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * spellToCast.spellRange), Color.white, spellToCast.Lifetime);
            //Debug.Log(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), spellToCast.spellRange, 0));
            Ray ray1 = new Ray(transform.position, transform.TransformDirection(Vector3.forward * spellToCast.spellRange));
            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) , spellToCast.spellRange, 0))
            if(Physics.Raycast(ray1, out RaycastHit hit, spellToCast.spellRange))
            {
                Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); //Ray is an infinite line but that doesnt matter as we've tested to see if its within the spells ray range already
                Physics.Raycast(ray, out other); //out keyword allows us to save data on the hit object for us to use
                collidedWith = other.transform.gameObject; //get the object we have hit with our raycast
               
                //Damage the enemy
                collidedWith.GetComponent<EnemyManager>().takeDamageFromSpell(spellToCast);

                //Set the object we just hit to be our most recently hit enemy for any spells that need it
                caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = collidedWith;

            }
        }

    }
}
