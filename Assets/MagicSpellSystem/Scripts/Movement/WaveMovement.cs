using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float amplitude;
    public float speed;
    public float magnitude;
    private float startDirection;
    public float frequency;

    // Start is called before the first frame update
    private void Start()
    {
        startDirection = transform.position.y;
    }


    void FixedUpdate()
    {
            //Sin Wave motion
            Vector3 pos = transform.position;
            float sin = amplitude * Mathf.Sin(Time.time * speed * frequency) * magnitude; 

            pos.y = startDirection + sin; //update our Y position
            transform.position = pos; //apply changes

    }
}
