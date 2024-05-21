using UnityEngine;

public class GunShootsDog : MonoBehaviour
{
    public int score = 0;
    public int shotsFired = 0;
    public BossLevelGameManager gameManager;
    public DialogManagerBossLevel dialogManager;

    void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !dialogManager.dialogStarted)
        {
            // Cast a ray from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            // Check if the ray hits an object
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                // Check if the hit object has the tag "Antis"
                if (hitObject.transform.CompareTag("Dog") && !hitObject.transform.CompareTag("Trunk"))
                {
                    score++;
                    gameManager.healthScript.SetHealth((float)1/10);
                }
            }
            shotsFired++;
        }
    }
}
