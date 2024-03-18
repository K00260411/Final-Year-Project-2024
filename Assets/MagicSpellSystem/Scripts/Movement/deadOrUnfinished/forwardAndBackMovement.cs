using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardAndBackMovement : MonoBehaviour
{
    private Vector3 intitalPosition;
    public float returnAtDistance;
    private Vector3 returnPosition;

    private void Awake()
    {
        intitalPosition = transform.position;
        returnPosition = transform.rotation * Vector3.forward;
        returnPosition = intitalPosition + returnPosition * returnAtDistance; 
            
            //transform.forward. + returnAtDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != returnPosition)
        {
            transform.Translate(Vector3.forward * GetComponent<Spell>().spellToCast.Speed * Time.deltaTime);
        }
        else
        {
            
            transform.Translate(Vector3.back * GetComponent<Spell>().spellToCast.Speed * Time.deltaTime);
        }
    }
}
