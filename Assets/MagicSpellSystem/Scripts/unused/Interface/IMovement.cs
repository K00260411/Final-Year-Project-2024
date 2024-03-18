using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    //Update our projectiles pathing based on the implementation of the interface
    void movement(GameObject projectile) { }
}
