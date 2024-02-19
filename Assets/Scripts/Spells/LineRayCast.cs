using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRayCast : MonoBehaviour
{
    //Maybe just port this into the update of spell.cs

    Spell spellAttachedTo;

    private void Awake()
    {
        spellAttachedTo = GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), spellAttachedTo.spellToCast.spellRange, 0)) 
        {
            //We have hit an object
        }
    }
}
