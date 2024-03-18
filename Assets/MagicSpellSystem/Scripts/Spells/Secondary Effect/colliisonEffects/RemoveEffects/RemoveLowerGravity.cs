using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLowerGravity : MonoBehaviour
{
    public float duration =10f;
    public float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //remove our components that we added via destory()
            ConstantForce cf = GetComponent<ConstantForce>();
            Destroy(cf);
            RemoveLowerGravity rlg = GetComponent<RemoveLowerGravity>();
            Destroy(rlg);
        }

    }
}
