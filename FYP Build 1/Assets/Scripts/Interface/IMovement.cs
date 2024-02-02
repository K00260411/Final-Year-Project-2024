using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    // Start is called before the first frame update
    void Start(){ }

    // Update is called once per frame
    void Update(){ }

    //Update our projectiles pathing based on the implementation of the interface
    void movement(GameObject projectile) { }
}
