using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour, IMovement
{
    /*    //Effectively making a sine wave
        public float frequency; //Used to determine the speed it will move back and forth at 
        public float amplitude; //used to determine the width of the wave
        public float cycleSpeed; //


        private Vector3 position;
        public Vector3 axis;*/

    public void Awake()
    {
        //position = transform.position;
    }

    public void Update()
    {
        /*        position += Vector3.down * Time.deltaTime * cycleSpeed;
                transform.position = position * Mathf.Sin(Time.time * frequency) * amplitude;

                if(transform.position.y < -7)
                {
                    Destroy(this.gameObject);
                }*/
    }

}


