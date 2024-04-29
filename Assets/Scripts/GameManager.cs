using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for changing scenes
using UnityEngine.UI;  // Needed if using UI elements

public class GameManager : MonoBehaviour
{
    public bool thrownDuckShot = false; // for future, the end will depend on it

    public float duckSpeed = 10f;
    public int duckySpawnProbability = 20;
    public bool isDuckySpawnable = true;

    public int score;
    public TextMeshProUGUI scoreText;

    public int scoreThresholdToProgress = 100;

    public Animator leftCurtainAnimator;
    public Animator rightCurtainAnimator;
    public Image fadeOverlay; // Assuming there's a UI Image to darken the screen

    private void Awake()
    {
        score = FindAnyObjectByType<GunShootsDuck>().score;
    }

    // Update is called once per frame
    void Update()
    {
        score = FindAnyObjectByType<GunShootsDuck>().score;
        scoreText.text = "Score: " + score;

        if (score >= scoreThresholdToProgress)
            StartCoroutine(CloseCurtainsAndChangeScene());
    }

    IEnumerator CloseCurtainsAndChangeScene()
    {
        // Trigger the curtain animations
        leftCurtainAnimator.SetBool("close", true);
        rightCurtainAnimator.SetBool("close", true);

        // Wait for the curtains to fully close (adjust the time to match the animation length)
        yield return new WaitForSeconds(2.0f);  // Assume animation takes 2 seconds to complete

        // Optionally fade the screen to black
        StartCoroutine(FadeOutScreen());

        // Load the next scene
        SceneManager.LoadScene("LevelFinished");
    }

    IEnumerator FadeOutScreen()
    {
        float duration = 1.0f; // Duration in seconds
        float currentTime = 0f;
        Color originalColor = fadeOverlay.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, currentTime / duration);
            fadeOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
