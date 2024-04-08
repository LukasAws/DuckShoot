using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlerpMoveDirection : MonoBehaviour
{
    public Vector2 startingPosition;
    public Vector2 endingPosition;

    public direction floatDirection;

    public float speed;

    static float t = 0;


    void Update()
    {
        float max = floatDirection == direction.horizontal ? endingPosition.x : endingPosition.y;
        float min = floatDirection == direction.horizontal ? startingPosition.x : startingPosition.y;

        if (floatDirection == direction.horizontal)
            transform.position = new Vector3(Mathf.SmoothStep(min, max, t), transform.position.y, transform.position.z);

        if (floatDirection == direction.vertical)
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(min, max, t), transform.position.z);


        // increase the interpolator
        t += 0.4f * Time.deltaTime * speed;


    }

    public enum direction
    {
        vertical, horizontal
    }

}
