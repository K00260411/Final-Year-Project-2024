using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingMovement : MonoBehaviour
{
   [HideInInspector] public GameObject targetObject;
   [HideInInspector] public GameObject actorAttachedTo;
   [HideInInspector] public Spell spellAttachedTo;

    private GameObject[] multipleObjectsInScene; //used to find our closest enemy
    [HideInInspector] public float permittedDistance = Mathf.Infinity; //How far can our object search for the closest enemy //just make infinity might be better
    private float closestRecordedDistance; //closest distance of an object instanse so far
    private GameObject gameTemp = null; //storage

    private void Awake()
    {
        //Get the game object for the sphere
        actorAttachedTo = this.gameObject;
        //Get the spell script we are currently attached to
        spellAttachedTo = GetComponent<Spell>();
        //targetObject = GameObject.Find("Enemy");

        //failsafe for if the player has not hit anything yet to prevent a null error
        if (spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject == null)
        {
            getClosestEnemy();
        }
        else
        {
            targetObject = spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject;
        }

    }


    // Update is called once per frame
    void Update()
    {
        movement();
    }

    public void movement(){

        //cleaner version
        if(targetObject != null)
        {
            //Track towards the target object
            transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, spellAttachedTo.spellToCast.Speed * Time.deltaTime);
            actorAttachedTo.transform.LookAt(targetObject.transform.position);
        }
        else
        {
            //no target object found so just move forward instead
            transform.Translate(Vector3.forward * spellAttachedTo.spellToCast.Speed * Time.deltaTime);
        }

    }

    public void getClosestEnemy()
    {
        //Find any enemy tagged object
        multipleObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");
        if(multipleObjectsInScene != null)
        {
            closestRecordedDistance = Mathf.Infinity; 
            gameTemp = null; //storage
            float currentDistance; //temp variable to hold the current temp objects distance
            foreach (GameObject tempObject in multipleObjectsInScene) //CHECK EACH ENEMY TAGGED OBJECT IN SCENE
            {
                currentDistance = Vector3.Distance(this.transform.position, tempObject.transform.position); //compare and save distances
                //if this is a shorter distance than recorded, save that distance and its associated object until the next shortest is found
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


