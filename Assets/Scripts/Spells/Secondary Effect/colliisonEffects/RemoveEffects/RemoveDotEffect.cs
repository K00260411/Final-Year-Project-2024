using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDotEffect : MonoBehaviour
{
    //public float duration;
    public float maxDuration;
    //public float currentDuration;
    public float dotDamage;
    public float timer = 0;

    //Make a boolean to check what game object we are attached to

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= maxDuration)
        {
            timer = timer + 1 * Time.deltaTime;
            if(gameObject.tag == "Enemy")
            {
                GetComponent<EnemyManager>().health -= dotDamage * Time.deltaTime * 1;
            }
            else
            {
                //Currently assuming other target is player with the example player manager script so add in any other tag checks if needed for your use case
                GetComponent<PlayerManager>().health -= dotDamage;
            }
        }
        else
        {
            RemoveDotEffect rdot = GetComponent<RemoveDotEffect>();
            Destroy(rdot);
        }
    }
}
