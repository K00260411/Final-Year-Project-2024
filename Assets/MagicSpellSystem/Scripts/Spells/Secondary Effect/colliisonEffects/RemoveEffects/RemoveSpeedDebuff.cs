using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSpeedDebuff : MonoBehaviour
{
    public float reduceSpeedBy;
    public float timer;
    public float debuffDuration;

    // Update is called once per frame
    void Update()
    {
        if (timer < debuffDuration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if(gameObject.tag == "Player")
            {
                GetComponent<PlayerManager>().moveSpeed = GetComponent<PlayerManager>().maxMoveSpeed;
            }
            else if (gameObject.tag == "Enemy")
            {
                GetComponent<EnemyManager>().moveSpeed = GetComponent<EnemyManager>().maxMoveSpeed;
            }
            else
            {

            }

            //remove our components via destory()
            RemoveSpeedDebuff rsd = GetComponent<RemoveSpeedDebuff>();
            Destroy(rsd);
            
        }
    }
}
