using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekMovement : MonoBehaviour
{
    public Rigidbody rigid;
    public Spell spell;
    Vector3 position;
    Vector3 velocity;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //Get the spell script we are currently attached to
        spell = GetComponent<Spell>();
        rigid = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        /*        //If the spell has a speed make it move forward, can allow for static positioned spells
                if (spell.spellToCast.Speed > 0)
                {
                    transform.Translate(Vector3.forward * spell.spellToCast.Speed * Time.deltaTime);
                }*/

        Vector3 desiredVelocity = targetObject.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * spell.spellToCast.Speed;

        Vector3 steering = desiredVelocity - velocity;

        velocity += steering;
        transform.position += velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}
