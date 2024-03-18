using UnityEngine;

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
        // smoothly move the object from min to max
        this.transform.position = new Vector3(Mathf.SmoothStep(min, max, t), transform.position.y, transform.position.z);

        // increase the interpolator
        t += 0.4f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the right direction
        if (t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0.0f;
        }
    }
}
