using UnityEngine;

public class CrosshairDistanceChecker : MonoBehaviour
{
    public Transform crosshair;
    public Transform[] animators;

    void Update()
    {
        float closestDistance = Mathf.Infinity;
        foreach (Transform animator in animators)
        {
            float distance = Vector2.Distance(animator.position, crosshair.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        Debug.Log("Closest distance to animator: " + closestDistance);
    }
}
