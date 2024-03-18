using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    [HideInInspector] public float moveSpeed;
    public float maxMoveSpeed;
    [HideInInspector] public float attackSpeed; //not used

    //Enum of all accounted damage types
    public enum allElements { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder };



    [Header("Damage Interaction")]
    public List<allElements> weaknesses;
    public List<allElements> resistances;
    public List<allElements> immunities;

    void Start()
    {
        
        moveSpeed = maxMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //How an enemy interacts with the damage type of the players spells
    public void takeDamageFromSpell(SpellScriptableObject spellHitBy)
    {

        
        if (immunities.Contains((allElements)spellHitBy.spellDamageTyp))
        {
            return; //if immune just exit the function
        }
        //Check if the enemy is resistant to the damage type taken
        else if (resistances.Contains((allElements)spellHitBy.spellDamageTyp))
        {
            health -= spellHitBy.damage * 0.5f;
            return; //exit so we dont apply damage twice
        }
        //Check if the enemy is weak to the damage type taken
        else if (weaknesses.Contains((allElements)spellHitBy.spellDamageTyp))
        {
            health -= spellHitBy.damage * 1.5f;

        }
        else
        {
            //If not any of the above apply the basic amount of damage
            health -= spellHitBy.damage;
        }

    }
}
