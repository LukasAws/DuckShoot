using UnityEngine;

public class GunShootsDog : MonoBehaviour
{
    public int score = 0;
    public int shotsFired = 0;
    public BossLevelGameManager gameManager;
    public DialogManagerBossLevel dialogManager;
    private float lastTime;

    private AudioSource audioSource;
    public AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !dialogManager.dialogStarted && Time.time - lastTime > 0.5f)
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
                    gameManager.healthScript.SetHealth((float)gameManager.damageStrength/50);
                    audioSource.PlayOneShot(clips[1]);
                }
            }
            shotsFired++;
            audioSource.PlayOneShot(clips[0]);
            lastTime = Time.time;
        }
    }
}
