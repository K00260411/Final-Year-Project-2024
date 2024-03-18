using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health;
    [HideInInspector] public float moveSpeed;
    public float maxMoveSpeed;
    public float attackSpeed;


    private void Update()
    {
        if(health <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
    }

}
