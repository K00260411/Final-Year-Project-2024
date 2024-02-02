using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement(this.gameObject);
        movement();
    }

    public void movement()
    {

        //Get the current position of our target object and look at it
        Vector3 targetObjectPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
        this.transform.LookAt(targetObjectPos);

        float distanceToTarget = Vector3.Distance(targetObject.transform.position, this.transform.position);
        //If we arent close enough to hit the target keep moving forward towards it
        if (distanceToTarget > 0.5f)
        {
            this.transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
        }
    }
}
