using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMovement : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject actorAttachedTo;
    public Spell spellAttachedTo;

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
        targetObject = spellAttachedTo.caster.GetComponent<PlayerMagicSystem>().lastHitGameObject;
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

}


