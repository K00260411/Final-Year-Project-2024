using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [HideInInspector] public Spell spellAttachedTo;

    private void Awake()
    {
        //Get the spell script we are currently attached to
        spellAttachedTo = GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the spell has a speed make it move forward, can allow for static positioned spells
        if (spellAttachedTo.spellToCast.Speed > 0)
        {
            transform.Translate(transform.forward * spellAttachedTo.spellToCast.Speed * Time.deltaTime);
        }

        
    }
}
