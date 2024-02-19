using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public Spell spellAttachedTo;
    public GameObject actorAttachedTo;
    public GameObject[] projectileTransforms;
    private GameObject spellCaster;
    private Vector3 castPosition;
    public float rotationSpeed;



    private void Awake()
    {
        //Get the spell script we are currently attached to
        spellAttachedTo = GetComponent<Spell>();

        //Get the game object for the sphere
        actorAttachedTo = this.gameObject;

        //Get the objects position when cast
        spellCaster = GameObject.Find("Player"); //GetComponent<GameObject>();
        castPosition = spellCaster.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {

        //rotate around the point the spell has cast in a circle fashion
        actorAttachedTo.transform.RotateAround(castPosition, spellCaster.transform.up, rotationSpeed * Time.deltaTime);


        //transform.Translate(Vector3.forward * spellAttachedTo.spellToCast.Speed * Time.deltaTime);
        //It'd be better to have more than just one projectile maybe
/*        foreach (var item in projectileTransforms)
        {
            item.transform.RotateAround(castPosition, spellCaster.transform.up, rotationSpeed* Time.deltaTime);
        }
*/

    }
}
