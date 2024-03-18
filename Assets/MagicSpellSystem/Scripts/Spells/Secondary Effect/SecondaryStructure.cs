using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class SecondaryStructure : MonoBehaviour
{
    //Hold data on what entity casted our spell so we can apply the effects to them
    public GameObject caster;
    //Used to trigger the bonus effect
    public virtual void applyBonusEffect()
    {

    }
}
