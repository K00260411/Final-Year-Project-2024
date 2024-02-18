using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyMovement : MonoBehaviour
{
    public float speedToReduceTo;

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
        }

        //maybe adding a remove slow enemy component might not be ideal
    }
}
