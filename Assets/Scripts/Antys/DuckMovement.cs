using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public DuckSpawner duckSpawner;

    private RectTransform m_RectTransform;

    public bool isShot = false;
    private float x = 2;
    public float speed = 15f;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        x = 0.5f * speed * Time.deltaTime;
        if (duckSpawner.duckDirection == DuckSpawner.DuckDirection.Left)
            transform.position = new Vector3(transform.position.x - x, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

        if (
                (transform.position.x < -8f && duckSpawner.duckDirection == DuckSpawner.DuckDirection.Left) ||
                (transform.position.x > 8f && duckSpawner.duckDirection == DuckSpawner.DuckDirection.Right)
            )
            Destroy(this.gameObject);


        if (isShot)
        {
            transform.localRotation = Quaternion.Euler(Mathf.Lerp(0, 90, t), (1-(float)duckSpawner.duckDirection)*180, transform.localRotation.z);

            t += 2f * Time.deltaTime;

            if (t > 1f)
            {
                isShot = false;
            }
        }
    }

}
