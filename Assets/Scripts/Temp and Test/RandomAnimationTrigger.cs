using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RandomAnimationTrigger : MonoBehaviour
{

    public RectTransform crosshair;
    public List<Animator> animators;
    public float updateInterval = 2f; // Interval in seconds

    private BossLevelGameManager gameManager;


    internal bool isFetching = false;

    private void Start()
    {
        gameManager = GetComponent<BossLevelGameManager>();
        InvokeDogs();
    }

    public void InvokeDogs()
    {
        foreach (var animator in animators)
        {
            animator.enabled = true;
        }

        {
            InvokeRepeating("UpdateMoveTrigger", 0f, updateInterval);
        }


        isFetching = false; // Reset fetching flag
    }

    private void UpdateMoveTrigger()
    {
        if (!gameManager.dialogManager.dialogStarted)
        {
            List<float> probabilities = new List<float>();
            foreach (Animator animator in animators)
            {
                RectTransform animatorRect = animator.GetComponent<RectTransform>();
                float distance = Vector2.Distance(animatorRect.position, crosshair.position);
                //Debug.Log(animator.name + " : " + distance);
                float probability = distance; // Inverse scaling
                probabilities.Add(probability);
            }

            // Select animator based on probabilities
            int selectedIndex = SelectAnimator(probabilities);

            // Trigger Move in the selected animator
            for (int i = 0; i < animators.Count; i++)
            {
                if (i == selectedIndex)
                    animators[i].SetTrigger("Move");

                //Debug.Log("selected " + selectedIndex);
            }
        }
        // Calculate probabilities
        
    }

    private int SelectAnimator(List<float> probabilities)
    {
        // Normalize probabilities
        float sum = 0;
        int a = 0;
        foreach (float probability in probabilities)
        {
            sum += probability;
            //Debug.Log(animators[a].name + " : probability : " + probability);
            a++;
        }
        for (int i = 0; i < probabilities.Count; i++)
        {
            probabilities[i] /= sum;
            //Debug.Log(animators[i].name + " : probability : " + probabilities[i]);
        }

        // Select animator based on probabilities
        float randomValue = Random.value;
        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilities.Count; i++)
        {
            cumulativeProbability += (probabilities[i]);
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        // This should never happen, but just in case
        return probabilities.Count - 1;
    }

    public void StopAndPlayDialog()
    {
        CancelInvoke();
        if(!gameManager.duckMovement.isTouchingGround)
        updateInterval++;

        foreach (var animator in animators)
        {
            animator.speed *= 0.8f;
        }




        if (gameManager.duckMovement.isTouchingGround && !isFetching)
        {
            float minDistance = float.MaxValue;
            GameObject selectedAnimator = new GameObject(); //temp
            if (!gameManager.dialogManager.dialogStarted)
            {
                List<float> probabilities = new List<float>();
                foreach (var animator in animators)
                {
                    RectTransform animatorRect = animator.GetComponent<RectTransform>();
                    float distance = Vector2.Distance(animatorRect.anchoredPosition, gameManager.duckMovement.GetComponent<RectTransform>().anchoredPosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        selectedAnimator = animator.gameObject;
                    }
                }

            }
        }


        InvokeRepeating("UpdateMoveTrigger", 4f, updateInterval);


    }
    public IEnumerator FetchDuck()
    {
        CancelInvoke();
        if (isFetching) yield break; // Check if already fetching

        isFetching = true;
        gameManager.duckMovement.hasTouchedGround = true;

        GameObject selectedAnimator = null;
        float minDistance = float.MaxValue;

        // Find the nearest animator to the duck
        foreach (var animator in animators)
        {
            RectTransform animatorRect = animator.GetComponent<RectTransform>();
            float distance = Vector2.Distance(animatorRect.anchoredPosition, gameManager.duckMovement.GetComponent<RectTransform>().anchoredPosition);
            if (distance < minDistance)
            {
                animator.enabled = false;
                minDistance = distance;
                selectedAnimator = animator.gameObject;
            }
        }

        // Store initial and final positions
        Vector2 initialPos = selectedAnimator.GetComponent<RectTransform>().anchoredPosition;
        Vector2 finalPos = gameManager.duckMovement.GetComponent<RectTransform>().anchoredPosition;

        // Smooth movement towards the duck
        float elapsedTime = 0f;
        float duration = 1f; // Adjust duration as needed
        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
            //Debug.Log(Vector2.Distance(selectedAnimator.GetComponent<RectTransform>().anchoredPosition, gameManager.duckMovement.GetComponent<RectTransform>().anchoredPosition));

            //Debug.Log(selectedAnimator.name);
            selectedAnimator.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(initialPos, finalPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        selectedAnimator.GetComponent<RectTransform>().anchoredPosition = finalPos; // Ensure final position is exact

        // Wait for a short duration
        yield return new WaitForSeconds(0.5f);

        // Parent the duck to the selected animator

        gameManager.spawnTime = Time.time;
        Destroy(gameManager.duckMovement.gameObject);

        // Smooth movement back to initial position
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
            selectedAnimator.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(finalPos, initialPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        selectedAnimator.GetComponent<RectTransform>().anchoredPosition = initialPos; // Ensure initial position is exact



        InvokeDogs();

        
    }
}
