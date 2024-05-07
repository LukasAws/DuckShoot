using UnityEngine;
using UnityEngine.EventSystems;

public class RandomAnimationTrigger : MonoBehaviour
{
    public Animator[] animators; // Assign animators in Unity Inspector
    public string triggerParameter = "Move"; // Name of the trigger parameter in the animator
    public float maxDistance = 500f; // Maximum distance at which an animation can be triggered

    private bool isAnimating = false;

    void Update()
    {
        if (!isAnimating)
        {
                Debug.Log("Test - 1");
                // Cast a ray from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    Debug.Log("Test - 2");
                    // Check if the hit object is one of our UI game objects
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        Debug.Log("Test - 3");
                        // Calculate distance to cursor
                        float distanceToCursor = Vector3.Distance(transform.position, hit.point);

                        // Calculate the likelihood of triggering an animation based on distance
                        float likelihood = 1 - Mathf.Clamp01(distanceToCursor / maxDistance);

                        // Randomly select an animator index based on likelihood
                        int animatorIndex = Random.Range(0, animators.Length);
                        float randomValue = Random.value;
                        if (randomValue < likelihood)
                        {
                            // Trigger the animation for the selected animator
                            animators[animatorIndex].SetBool(triggerParameter, true);
                            isAnimating = true;
                        }
                    }
                }
        }
    }

    // Animation event called by animation timeline
    public void AnimationComplete()
    {
        // Reset animation trigger for all animators
        foreach (Animator animator in animators)
        {
            animator.SetBool(triggerParameter, false);
        }
        isAnimating = false;
    }
}
