using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItemBase : MonoBehaviour
{
    [HideInInspector] public GameObject equipedTo;
    public Spell spellToCast;
    public int MaximumUses;
    [HideInInspector] public int currentUses;
    [HideInInspector] public Transform castPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentUses = MaximumUses;
    }


    public void castSpell()
    {
        //If we have charges remaining, decrement and cast the assoicated spell
        if(currentUses > 0)
        {
            currentUses--;


            if(equipedTo.tag == "Enemy")
            {
                castPoint = equipedTo.transform; //if you want the enemy to cast from its own dedicated cast point add one to your manager script
                spellToCast.caster = equipedTo;
            }

            spellToCast.caster = equipedTo; //save the casters details
            Instantiate(spellToCast, castPoint.position, castPoint.rotation);  //Create the spell object  at the players casting position
        }
        if (currentUses < 0) currentUses = 0;


        //CASTING THE SPELL ALSO GIVES A TON OF NULL REFERENCE Exception : SerialisedObject of serialized property has been disposed

    }

    //Call whenever best suits your games balance
    public void rechargeUses()
    {
        currentUses = MaximumUses;
    }

    //If its something in the gameworld to pick up and not outright given
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //GameObject clonedMagicItem = GameObject.Instantiate(this.gameObject);
            other.gameObject.GetComponent<PlayerMagicSystem>().addMagicItem(this.gameObject); //need to somehow make a clone of this without spawning it in the gameworld
            //Destroy(this.gameObject); //cant destroy this as it also destroys the one in PlayerMagicSystems array
        }
    }
}
