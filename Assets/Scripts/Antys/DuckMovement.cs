using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public DuckSpawner duckSpawner;

    private float x;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = 0.2f * speed * Time.deltaTime;
        if (duckSpawner.duckDirection == DuckSpawner.DuckDirection.Left)
            transform.position = new Vector3(transform.position.x - x, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

        Debug.Log(Time.time);

        if (
                (transform.position.x < -8f && duckSpawner.duckDirection == DuckSpawner.DuckDirection.Left) ||
                (transform.position.x > 8f && duckSpawner.duckDirection == DuckSpawner.DuckDirection.Right)
            )
            Destroy(this.gameObject);

        Debug.Log(this.name+" : "+duckSpawner.duckDirection);
    }

}
