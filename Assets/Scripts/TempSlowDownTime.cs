using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSlowDownTime : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 0.3f;
        } else Time.timeScale = 1f;

    }
}
