using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int timesToBounce;
    private Vector3 velocity;
    private Vector3 direction;
    private int bouncedCounter = 0;
    private float movementSpeed = 10f;
    private float reflection = 0.8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Awake");
    }


    //hmm

    private void OnCollisionEnter(Collision collision)
    {
/*        Debug.Log("collision");
        if(bouncedCounter <= timesToBounce)
        {
            //reflects the vector velocity.normalized off the plane contacts[o].normal
            direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal);
            movementSpeed = velocity.magnitude;
            rb.velocity = direction * Mathf.Max(movementSpeed, 0); //multiply the direction by the movement speed if its greater than 0 else * by 0
            bouncedCounter++;
            Debug.Log("counterIncrement");
        }
        else
        {
            Destroy(gameObject); //if you want the projectile to keep moving after it bounces for the last time comment this out
            return;
        }*/



    }
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //can i somehow get the length of a generic collider as the distance input of the raycast?
        if (Physics.Raycast(ray, out hit, movementSpeed * Time.deltaTime))  //GetComponent<SphereCollider>().radius))
        {
            Debug.Log("cast");
            Vector3 reflectionDirection = Vector3.Reflect(ray.direction, hit.normal);
            float rotationAmount = 90 - Mathf.Atan2(reflectionDirection.x, reflectionDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rotationAmount, 0);
            /*            velocity = transform.forward * movementSpeed;
                        Vector3 reflectionDirection = Vector3.Reflect(velocity, hit.normal);
                        velocity = reflectionDirection * reflection;*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.attachedRigidbody.con
        Debug.Log("trigger");
        
    }

    //best to use last update to make sure we get the most to date and final data for this frame
    private void LateUpdate()
    {
        velocity = rb.velocity;
    }
}
