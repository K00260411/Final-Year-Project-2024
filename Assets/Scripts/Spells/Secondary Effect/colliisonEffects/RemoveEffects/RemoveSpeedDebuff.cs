using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSpeedDebuff : MonoBehaviour
{
    public float reduceSpeedBy;
    public float timer;
    public float debuffDuration;

    private void Start()
    {
/*        if(gameObject.tag == "Enemy")
        {
            GetComponent<EnemyManager>().moveSpeed =-reduceSpeedBy;
            if (GetComponent<EnemyManager>().moveSpeed <= 0) GetComponent<EnemyManager>().moveSpeed = 1;
        }
        else if (gameObject.tag == "Player")
        {
            GetComponent<PlayerManager>().moveSpeed = -reduceSpeedBy;
            if (GetComponent<PlayerManager>().moveSpeed <= 0) GetComponent<EnemyManager>().moveSpeed = 1;
        }
        else
        {
            //include other tag checks as needed
        }*/
    }

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

            RemoveSpeedDebuff rsd = GetComponent<RemoveSpeedDebuff>();
            Destroy(rsd);
            //remove our components
        }
    }
}
