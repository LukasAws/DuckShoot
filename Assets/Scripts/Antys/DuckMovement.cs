using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public DuckSpawner duckSpawner;

    private float x = 2;
    [SerializeField]
    float speed = 15f;
    float t = 0;

    public bool isShot = false;
    public bool isChild = false;
    public bool isThrowable = false;

    public DuckSpawner.DuckDirection duckDirection;

    public bool beMovable = true;

    Quaternion originalRot;
    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (beMovable)
        {
            x = GetAppropriateSpeed();

            if (duckSpawner.spawnerDuckDirection == DuckSpawner.DuckDirection.Left)
                transform.position = new Vector3(transform.position.x - x, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);

            if (
                    (transform.position.x < -8f && duckSpawner.spawnerDuckDirection == DuckSpawner.DuckDirection.Left) ||
                    (transform.position.x > 8f && duckSpawner.spawnerDuckDirection == DuckSpawner.DuckDirection.Right)
                )
                Destroy(this.gameObject); // when reached edge - destroy it

            if (t < 1f && isShot && !isChild)
            {
                transform.localRotation = Quaternion.Euler(Mathf.Lerp(0, 90, t), (1 - (float)duckSpawner.spawnerDuckDirection) * 180, transform.localRotation.z);

                t += 2f * Time.deltaTime;
            }
        }
        
    }

    private void LateUpdate()
    {
        if (this.isChild)
            transform.rotation = originalRot;
    }

    private float GetAppropriateSpeed()
    {
        if (isChild && this.transform.parent.GetComponent<DuckMovement>().isShot) // if mother shot - speed up
            return 0.3f * speed * Time.deltaTime;
        else if (isChild && !this.transform.parent.GetComponent<DuckMovement>().isShot) // if not - do nothing
            return 0;
        
        return 0.5f * speed * Time.deltaTime; // return normal speed
    }


}
