using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour, IMovement
{
    //rotate our object side to side 
    Quaternion rotationQuaternion = Quaternion.Euler(new Vector3(0, 20, 0));
    Quaternion rotationQuaternion2 = Quaternion.Euler(new Vector3(0, -20, 0));
    public Spell spellAttachedTo;
    // Start is called before the first frame update
    void Start()
    {
        //Get the spell script we are currently attached to
        spellAttachedTo = GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {
        movement2();
    }

    void movement(GameObject projectile)
    {
        //Update our projectiles movement in a zig zag manner
        projectile.transform.rotation = rotationQuaternion;

        transform.Translate(Vector3.forward * spellAttachedTo.spellToCast.Speed * Time.deltaTime);

    }

    void movement2()
    {
        //Update our projectiles movement in a zig zag manner
        transform.rotation = rotationQuaternion;

        transform.Translate(Vector3.forward * spellAttachedTo.spellToCast.Speed * Time.deltaTime);

        

    }
}
