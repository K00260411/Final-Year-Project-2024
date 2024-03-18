using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyMovement : MonoBehaviour
{
    public float speedToReduceTo;
    public float debuffDuration;

    private void OnTriggerEnter(Collider other)
    {
        //Reduce the targets movement speed by specified amount and default to 1 if it goes too low
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyManager>().moveSpeed = speedToReduceTo;
            if(other.gameObject.GetComponent<EnemyManager>().moveSpeed <= 0)
            {
                other.gameObject.GetComponent<EnemyManager>().moveSpeed = 1;
            }
        }
        else if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().moveSpeed = speedToReduceTo;
            if (other.gameObject.GetComponent<PlayerManager>().moveSpeed <= 0)
            {
                other.gameObject.GetComponent<PlayerManager>().moveSpeed = 1;
            }
        }
        else
        {
            //ad in any other tage checks you need to include
        }

        
    }
}
