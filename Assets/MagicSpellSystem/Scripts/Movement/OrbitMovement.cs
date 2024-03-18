using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    [HideInInspector] public Spell spellAttachedTo;
    [HideInInspector] public GameObject actorAttachedTo;
    private GameObject spellCaster;
    private Vector3 castPosition;
    public float rotationSpeed; //what speed to rotate around the caster



    private void Awake()
    {
        //Get the game object for the sphere
        actorAttachedTo = this.gameObject;

        //Get the objects position when cast
        spellCaster = GetComponent<Spell>().caster;
        castPosition = spellCaster.transform.position;
        actorAttachedTo.transform.parent = spellCaster.transform; //set the catser as a parent transform to ensure it rotates properly when moving
    }

    // Update is called once per frame
    public void Update()
    {
        castPosition = spellCaster.transform.position;
        //rotate around the point the spell has cast in a circle fashion
        actorAttachedTo.transform.RotateAround(castPosition, spellCaster.transform.up, rotationSpeed * Time.deltaTime);

    }
}
