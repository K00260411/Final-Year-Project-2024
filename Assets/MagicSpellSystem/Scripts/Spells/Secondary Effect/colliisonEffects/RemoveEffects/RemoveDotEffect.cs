using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDotEffect : MonoBehaviour
{
    public float maxDuration;
    public float dotDamage;
    public float timer = 0;


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
            else if (gameObject.tag == "Player")
            {
                //Currently assuming other target is player with the example player manager script so add in any other tag checks if needed for your use case
                GetComponent<PlayerManager>().health -= dotDamage;
            }
        }
        else
        {
            //Remove our component via destroy()
            RemoveDotEffect rdot = GetComponent<RemoveDotEffect>();
            Destroy(rdot);
        }
    }
}
