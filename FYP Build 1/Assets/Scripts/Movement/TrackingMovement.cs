using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMovement : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject actorAttachedTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Get the game object for the sphere
        actorAttachedTo = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    public void movement(){

        //Get the current position of our target object and look at it
        Vector3 targetObjectPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
        actorAttachedTo.transform.LookAt(targetObjectPos);

        float distanceToTarget = Vector3.Distance(targetObject.transform.position, this.transform.position);
        //If we arent close enough to hit the target keep moving forward towards it
        if(distanceToTarget > 1f)
        {
            actorAttachedTo.transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
        }
    }

}


