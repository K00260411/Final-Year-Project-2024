using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingMovement : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject actorAttachedTo;
    public Spell spellAttachedTo;

    private GameObject[] multipleObjectsInScene; //used to find our closest enemy
    public float permittedDistance = Mathf.Infinity; //How far can our object search for the closest enemy //just make infinity might be better
    private float closestRecordedDistance; //closest distance of an object instanse so far
    private GameObject gameTemp = null; //storage

    // Start is called before the first frame update
    void Start()
    {

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

    }


    // Update is called once per frame
    void Update()
    {
        movement();
    }

    public void movement(){

        /*        //Get the current position of our target object and look at it
                Vector3 targetObjectPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
                Debug.Log(targetObjectPos);
                actorAttachedTo.transform.LookAt(targetObjectPos);

                float distanceToTarget = Vector3.Distance(targetObject.transform.position, this.transform.position);
                //If we arent close enough to hit the target keep moving forward towards it
                if(distanceToTarget > 0.5f)
                {
                    actorAttachedTo.transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
                }*/

        //cleaner version
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, spellAttachedTo.spellToCast.Speed * Time.deltaTime);
        actorAttachedTo.transform.LookAt(targetObject.transform.position);
    }

    public void getClosestEnemy()
    {
        multipleObjectsInScene = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(multipleObjectsInScene.Length);
        //closestRecordedDistance = permittedDistance; //set permitted to infinity for now i guess
        closestRecordedDistance = Mathf.Infinity;
        //Debug.Log(closestRecordedDistance);
        gameTemp = null; //storage
        float currentDistance; //temp variable to hold the current temp objects distance
        foreach (GameObject tempObject in multipleObjectsInScene) //CHECK EACH ENEMY TAGGED OBJECT IN SCENE
        {
            currentDistance = Vector3.Distance(this.transform.position, tempObject.transform.position); //compare and save distances
            //Debug.Log(currentDistance);
            //Using temp variables 
            if(currentDistance < closestRecordedDistance)
            {
                closestRecordedDistance = currentDistance;
                this.gameTemp = tempObject;
                //Debug.Log(gameTemp);
                
            }
        }
        //Save our found closet game object
        targetObject = gameTemp;
        //Debug.Log(targetObject);
        spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject = targetObject;
    }

}


