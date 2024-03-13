using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RemoveHealing : MonoBehaviour
{
    public float maxDuration;
    public float currentDuration;
    public float hotHealing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentDuration <= maxDuration)
        {
            currentDuration = currentDuration + 1 * Time.deltaTime;
            if (gameObject.tag == "Enemy")
            {
                //assume included enemy manager script, replace if needed
                GetComponent<EnemyManager>().health += hotHealing * Time.deltaTime * 1;
            }
            else
            {
                //Currently assuming other target is player with the example player manager script so add in any other tag checks if needed for your use case
                GetComponent<PlayerManager>().health -= hotHealing;
            }
        }
        else
        {
            RemoveHealing rhot = GetComponent<RemoveHealing>();
            Destroy(rhot);
        }
    }
}
