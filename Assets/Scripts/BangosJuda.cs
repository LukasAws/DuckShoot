using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BangosJuda : MonoBehaviour
{
    public bool atvirkscias;

    private float min = -1.372f;
    private float max = 0;

    private float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (atvirkscias)
        {
            float temp = max;
            max = min;
            min = temp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // animate the position of the game object...
        this.transform.position = new Vector3(Mathf.SmoothStep(min, max, t), transform.position.y, transform.position.z);

        // .. and increase the t interpolater
        t += 0.4f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0.0f;
        }
    }
}
