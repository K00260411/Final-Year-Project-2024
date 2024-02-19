using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : SecondaryStructure
{
    public float maxDuration;
    public float currentDuration;
    public float hotHealing;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //Get the spell script we are currently attached to
        spellToApplyTo = GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void applyBonusEffect()
    {

        if (currentDuration < maxDuration)
        {
            spellToApplyTo.collidedWith.GetComponent<EnemyManager>().health -= hotHealing;
            currentDuration += currentDuration * Time.deltaTime;

        }
    }
}
