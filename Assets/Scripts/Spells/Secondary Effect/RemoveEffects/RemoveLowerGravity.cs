using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLowerGravity : MonoBehaviour
{
    public float duration =10f;
    public float timer;
    public GameObject parent;

    // Start is called before the first frame update
    void awake()
    {
        //parent = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ConstantForce cf = GetComponent<ConstantForce>();
            Destroy(cf);
            //remove our components
            //Destroy(GetComponent<lowerGravity>()); not on the effected enemy, duh
            RemoveLowerGravity rlg = GetComponent<RemoveLowerGravity>();
            Destroy(rlg);
        }
/*        Debug.Log("In remove");
        Debug.Log(duration);*/
/*        Destroy(GetComponent<lowerGravity>(), duration);
        Destroy(GetComponent<RemoveLowerGravity>(), duration);*/

    }
}
