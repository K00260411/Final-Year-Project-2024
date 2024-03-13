using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float y0;
    public float amplitude;
    public float speed;
    public float magnitude;
    private float startY;
    public Vector3 temp;
    private Vector3 startingPos;
    public float frequency;
    public bool change = false;


    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        /*        y0 = transform.position.y;
                temp = transform.position;
                temp.y = y0 + amplitude * Mathf.Sin(speed * Time.deltaTime);
                transform.position = temp;*/
        temp = transform.position;
        //temp.y = Mathf.Cos(Time.deltaTime * frequency) * amplitude * frequency;
/*        if (change)
        {
            temp.y += Mathf.Sin(Time.time * speed) * amplitude;
            change = false;
        }
        else
        {
            temp.y -= Mathf.Sin(Time.time * speed) * amplitude;
            change = true;
        }*/
        transform.position = temp;
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);



    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.y * frequency) * magnitude;

        pos.y = 0 + sin;

        transform.position = pos;
    }
}
