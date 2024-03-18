using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZigZagMovement : MonoBehaviour, IMovement
{

    public float amplitude;
    public float speed;
    public float magnitude;
    public float frequency;
    private float startDirection;

    public enum axis { Z, X};
    public axis axisToApplyTo;


    // Start is called before the first frame update
    private void Start()
    {
        if(axisToApplyTo == axis.X) //|| transform.position.
        {
            startDirection = transform.position.x;
        }
        else if(axisToApplyTo == axis.Z)
        {
            startDirection = transform.position.x;
        }
        
    }

    //zigzag not working atm

    void FixedUpdate()
    {

        Vector3 pos = transform.position;

        float sin = amplitude * Mathf.Sin(Time.time * speed * frequency) * magnitude;

        if (axisToApplyTo == axis.X)
        {
            pos.x = startDirection + sin;
        }
        else if(axisToApplyTo == axis.Z)
        {
            pos.z = startDirection + sin;
        }

        transform.position = pos;




    }

}


