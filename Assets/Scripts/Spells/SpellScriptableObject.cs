using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spell", menuName ="Spells")] //allows for a scriptable object asset to be created via the unity editor
public class SpellScriptableObject : ScriptableObject
{
    //Define all of our spells required characteristics
    [Header("Spell Details")]
    public float ManaCost = 5f;
    public float Lifetime = 5f;
    public float Speed = 15f;
    public float damage = 10f;
    public float spellRadius = 2f; //size of spell sphere or other shape, not its range
    //Enum damage type
    public enum damageTypes { acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder }
    public damageTypes spellDamageTyp;
    //Enum attack type
    public enum attackTypes { cone, line, radius, cube, bolt }
    public attackTypes spellAttackType;
    //String name
    public string spellName;
    //int cost
    public int spellCost;
    //float range
    public float spellRange;
    //float casting time
    public float castTime;
    //String effect text
    public string effectText;

    //UI Icon
    public Sprite spellArtIcon;

}
