using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLowerGravity : MonoBehaviour
{
    public float duration;
    public float timer;
    public GameObject parent;

    // Start is called before the first frame update
    void awake()
    {
        parent = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*        if(timer < duration)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    //remove our components
                    Destroy(this.gameObject.GetComponent<lowerGravity>());
                    Destroy(this.gameObject.GetComponent<RemoveLowerGravity>());
                }*/
        Destroy(this.gameObject.GetComponent<lowerGravity>(), duration);
        Destroy(this.gameObject.GetComponent<RemoveLowerGravity>(), duration);

    }
}
