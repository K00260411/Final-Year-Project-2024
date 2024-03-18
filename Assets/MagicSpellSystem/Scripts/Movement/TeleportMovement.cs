using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour
{

    [HideInInspector] public GameObject targetObject;
    [HideInInspector] public GameObject actorAttachedTo;
    [HideInInspector] public Spell spellAttachedTo;

    public float verticalOffset; //how high above the enemy should the projectile teleport to

    private GameObject[] multipleObjectsInScene; //used to find our closest enemy
    public float permittedDistance = Mathf.Infinity; //How far can our object search for the closest enemy //just make infinity might be better
    private float closestRecordedDistance; //closest distance of an object instanse so far
    private GameObject gameTemp = null; //storage


    private void Update()
    {
        transform.Translate(Vector3.down * spellAttachedTo.spellToCast.Speed * Time.deltaTime); //always move downwards after we teleported above the enemy
    }

    private void Awake()
    {
        //Get the game object for the sphere
        actorAttachedTo = this.gameObject;
        //Get the spell script we are currently attached to
        spellAttachedTo = GetComponent<Spell>();
        //targetObject = GameObject.Find("Enemy");

        //Add in a a failsafe for if the player has not hit anything yet to prevent a null error
        if (spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject == null)
        {
            getClosestEnemy();
        }
        else
        {
            targetObject = spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject;
        }

        //in case there are no enemy tagged entities in scene
        if(targetObject != null)
        {
            transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + verticalOffset, targetObject.transform.position.z);
        }
    }

    public void getClosestEnemy()
    {
        multipleObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");

        if(multipleObjectsInScene != null)
        {
            closestRecordedDistance = Mathf.Infinity;
            gameTemp = null; //storage
            float currentDistance; //temp variable to hold the current temp objects distance
            foreach (GameObject tempObject in multipleObjectsInScene) //CHECK EACH ENEMY TAGGED OBJECT IN SCENE
            {
                currentDistance = Vector3.Distance(this.transform.position, tempObject.transform.position); //compare and save distances

                if (currentDistance < closestRecordedDistance)
                {
                    closestRecordedDistance = currentDistance;
                    this.gameTemp = tempObject;

                }
            }
            //Save our found closet game object
            targetObject = gameTemp;
            spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = targetObject;
        }

    }
}
