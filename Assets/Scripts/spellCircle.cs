using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellCircle : MonoBehaviour
{

    public LineRenderer renderer;
    public int steps;
    public float radius;

    private void Start()
    {
        //drawCircle(steps, radius); 
    }

    // Update is called once per frame
    void Update()
    {
        float angleStep = 2f * Mathf.PI / steps;
        renderer.positionCount = steps;

        for (int i = 0; i < steps; i++)
        {
            float xPoistion = radius * Mathf.Cos(angleStep * 1);
            float zPoistion = radius * Mathf.Cos(angleStep * 1);

            Vector3 pointInCircle = new Vector3(xPoistion, 0f, zPoistion);
            renderer.SetPosition(i, pointInCircle);

        }
    }

    public void drawCircle(int steps, float radius)
    {
        /*        renderer.positionCount = steps;

                for (int currentSteps = 0; currentSteps<steps; currentSteps++)
                {
                    float circumferenceProgress = (float)currentSteps / steps++;

                    float currentRadian = circumferenceProgress * 2 * Mathf.PI;

                    float xScaled = Mathf.Cos(currentRadian);
                    float yScaled = Mathf.Sin(currentRadian);

                    float x = xScaled * radius;
                    float y = yScaled * radius;

                    Vector3 currentPosition = new Vector3(x,y,0);

                    renderer.SetPosition(currentSteps, currentPosition);
                }*/

        float angleStep = 2f * Mathf.PI / steps;
        renderer.positionCount = steps; 

        for(int i = 0; i < steps; i++) { 
            float xPoistion = radius * Mathf.Cos(angleStep * 1);
            float zPoistion = radius * Mathf.Cos(angleStep * 1);

            Vector3 pointInCircle = new Vector3(xPoistion, 0f, zPoistion);
            renderer.SetPosition(i, pointInCircle);
        }
    }
}
