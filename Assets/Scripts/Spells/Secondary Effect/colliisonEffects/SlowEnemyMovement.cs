using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyMovement : MonoBehaviour
{
    public float speedToReduceTo;
    public float debuffDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
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

        }

        
    }
}
