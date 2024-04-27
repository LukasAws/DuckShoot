using UnityEngine;

public class GunShootsDuck : MonoBehaviour
{
    public int score = 0;
    public DialogManager manager;

    void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !manager.dialogStarted)
        {
            // Cast a ray from the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hits an object
            if (hit.collider != null)
            {

                GameObject hitObject = hit.collider.gameObject;
                // Check if the hit object has the tag "Antis"
                if (hitObject.transform.CompareTag("Antis"))
                {
                    if (!hitObject.GetComponent<DuckMovement>().isShot && !hitObject.GetComponent<DuckMovement>().isChild)
                    {
                        score++;

                        hitObject.GetComponent<DuckMovement>().isShot = true;
                    }

                }
            }
        }
    }
}
