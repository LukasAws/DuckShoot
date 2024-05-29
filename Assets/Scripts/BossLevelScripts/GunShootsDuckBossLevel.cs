using UnityEngine;

public class GunShootsDuckBossLevel : MonoBehaviour
{
    public DialogManagerBossLevel manager;
    public int shotsFired = 0;
    public AudioClip duckShotClip;

    void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !manager.dialogStarted)
        {
            // Cast a ray from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            // Check if the ray hits an object
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                GameObject hitObject = hit.collider.gameObject;
                // Check if the hit object has the tag "Antis"
                if (hitObject.transform.CompareTag("Antis"))
                {
                    if (!hitObject.GetComponent<DuckMovementBossLevel>().isShot)
                    {
                        hitObject.GetComponent<DuckMovementBossLevel>().isShot = true;
                        GetComponent<AudioSource>().PlayOneShot(duckShotClip);
                    }

                }
            }
            shotsFired++;
        }
    }
}
