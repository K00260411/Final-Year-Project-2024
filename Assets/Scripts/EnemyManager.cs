using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    [HideInInspector]public float moveSpeed;
    public float maxMoveSpeed;
    public float attackSpeed;


    //Refactor this into something better
    public enum possibleResistances { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder };
    public enum possibleWeaknesses { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder };

    public enum possibleImmunities { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder };

    public enum allElements { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder };


    public List<possibleWeaknesses> listOfWeaknesses;
    public List<possibleResistances> listOfResistances;
    public List<possibleImmunities> listOfImmunities;

    public bool takingDOT = false;
    public float dotAmount = 0;
    public float dotDamageDuration = 0;
    public float dotDamageTemp = 0;

    void Start()
    {
        health = 100;
        moveSpeed = maxMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Health: " + health);

/*        if(takingDOT == true)
        {
            takeSpellDOT(dotAmount, dotDamageDuration);
        }*/

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void lowerSpeed()
    {

    }

/*    public void triggerDOT(float damage, float duration)
    {
        //StartCoroutine(takeDOT(damage, duration));
        float totalDamage = damage * duration;
        takingDOT = true;
        takeSpellDOT(damage, duration);
    }*/

    public void takeDamageFromSpell(SpellScriptableObject spellHitBy )
    {
/*        //Might be able to swap the arrays to a list so i can use the .contains function?
        for (int i = 0; i < enemyImmunities.Length; i++) //switch enemy immunites to some static variable shared between classes that list all possible damage types, eI is just being used as a stand in //or maybe put this into an enemy scriptable?
        {
            //Check if the enemy is weak to the damage type taken
            if (enemyWeaknesses[i].ToString() == spellHitBy.spellDamageTyp.ToString())
            {
                health -= spellHitBy.damage * 1.5f;
            }
            //Check if the enemy is resistant to the damage type taken
            else if (enemyResistances[i].ToString() == spellHitBy.spellDamageTyp.ToString())
            {
                health -= spellHitBy.damage * 0.5f;
            }

            //Check if the enemy is immune to the damage type taken
            else if(enemyImmunities[i].ToString() == spellHitBy.spellDamageTyp.ToString())
            {
                return; //exit the function
            }

            //THIS METHOD WILL CURRENTLY FAIL IF ALL DO NOT SHARE THE SAME LENGTH
        }*/

        //Check if the enemy is immune to the damage type taken
        if (listOfImmunities.Contains((possibleImmunities)spellHitBy.spellDamageTyp))
        {
            return; //if immune just exit the function
        }
        //Check if the enemy is resistant to the damage type taken
        else if (listOfResistances.Contains((possibleResistances)spellHitBy.spellDamageTyp))
        {
            health -= spellHitBy.damage * 0.5f;
            return; //exit so we dont apply damage twice
        }
        //Check if the enemy is weak to the damage type taken
        else if (listOfWeaknesses.Contains((possibleWeaknesses)spellHitBy.spellDamageTyp))
        {
            health -= spellHitBy.damage * 1.5f;
            return; //exit so we dont apply damage twice
        }


        //If not any of the above apply the basic amount of damage
        health -= spellHitBy.damage; 
    }

/*    IEnumerator takeDOT(float damage, float duration)
    {
        float i = 0;
        while (true) //true is just while the coroutine is running
        {
*//*            health -= damage; //*Time.deltaTime;*//*
            i = +1;
            Debug.Log("Health:" + health);
            //yield return new WaitForSeconds();
            if (i >= duration)
            {
                yield return null;
            }
            yield return null;
        }
        
        
    }

    public void takeSpellDOT(float dotAmountIn, float duration)
    {
        if(dotAmount == null || dotDamageTemp == null)
        {
            dotAmount = dotAmountIn;
            dotDamageTemp = duration;
            //Debug.Log(dotAmount);
        }
        takingDOT = true;
        health -= dotAmount * Time.deltaTime * 1;
        dotDamageDuration += dotDamageTemp * Time.deltaTime * 1;
        //Debug.Log(dotDamageDuration);
        //Debug.Log(duration);

        if(dotDamageDuration >= dotDamageTemp)
        {
            takingDOT = false;
            dotAmount = 0;
            dotDamageDuration = 0;
        }
    }*/
}
