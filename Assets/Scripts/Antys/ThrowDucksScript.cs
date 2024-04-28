using UnityEngine;

public class DogAnimationController : MonoBehaviour
{
    public Animator dogAnimator;
    public Animator duckAnimator;
    public GameObject duck;

    private bool duckThrown = false;


    private void Update()
    {
        // Check if the dog animation reaches halfway
        if (dogAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && !duckThrown && dogAnimator.GetCurrentAnimatorStateInfo(0).IsName("DogMoves"))
        {
            // Unparent the duck from the dog
            duck.transform.SetParent(duck.transform.parent.transform.parent, true);
            duck.transform.position = dogAnimator.transform.position;
            // Trigger the "Middle" animation of the duck
            duckAnimator.SetTrigger("Middle");
            // Set duckThrown to true to avoid repeating this process
            duckThrown = true;
        }

        if (duckAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && duckThrown && duckAnimator.GetCurrentAnimatorStateInfo(0).IsName("ThrowableDuckAnimation"))
        {
            // Unparent the duck from the dog
            duck.transform.SetParent(dogAnimator.transform, true);
            // Trigger the "Middle" animation of the duck
            duckThrown = false;
            duck.GetComponent<DuckMovement>().isShot = false;
        }
    }

    public void TriggerDogMove()
    {
        // Trigger the "Move" animation of the dog
        dogAnimator.SetTrigger("Move");
    }
}
