using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int shotsFired;
    public int thrownDucksShot;

    public int scoreThresholdToProgress = 100;


    public Animator leftCurtainAnimator;
    public Animator rightCurtainAnimator;

    private GunShootsDuck gunScript;
    public Image fadeOverlay; // Assuming there's a UI Image to darken the screen

    public float probabilityOfThrownDuck = 0;
    public float randomFloat;
    public DogAnimationController dogAC;

    public GameObject menu;

    private bool gameStopped = false;

    public DialogManager dialogManager;
    private float orgSpeed;

    internal ButtonsScript buttonScript;

    private void Start()
    {
        buttonScript = FindObjectOfType<ButtonsScript>();
        gunScript = FindAnyObjectByType<GunShootsDuck>();
        score = gunScript.score;
        orgSpeed = dialogManager.orgSpeed;

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("GlobalVolume");
    }

    // Update is called once per frame
    void Update()
    {
        score = FindAnyObjectByType<GunShootsDuck>().score;
        scoreText.text = "Score: " + score;

        if (score >= scoreThresholdToProgress)
            StartCoroutine(CloseCurtainsAndFadeOut());

        if(randomFloat < probabilityOfThrownDuck && !dialogManager.dialogStarted && score >= 40)
        {
            probabilityOfThrownDuck = 0;
            dogAC.TriggerDogMove();
        } else if (score < 40)
        {
            probabilityOfThrownDuck = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameStopped = !gameStopped;
            Time.timeScale = gameStopped ? 0 : 1;
            ImmediateMenu();
        }

        //if (buttonScript.levelID == 0)
        //{
        //    buttonScript.levelID = 2;
        //    PlayerPrefs.SetInt("LevelID", 2);
        //}

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("GlobalVolume") - fadeOverlay.color.a * PlayerPrefs.GetFloat("GlobalVolume");

    }

    public IEnumerator CloseCurtainsAndFadeOut()
    {
        // Trigger the curtain animations
        leftCurtainAnimator.SetBool("close", true);
        rightCurtainAnimator.SetBool("close", true);

        // Wait for the curtains to fully close (adjust the time to match the animation length)

        //yield return new WaitForSeconds(2.0f);

        // Optionally fade the screen to black
        StartCoroutine(FadeOutScreen());

        yield return new WaitForSeconds(2.0f);  // Assume animation takes 2 seconds to complete

        shotsFired = gunScript.shotsFired;
        thrownDucksShot = gunScript.thrownDucksShot;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("ShotsFired", shotsFired);
        PlayerPrefs.SetInt("ThrownDucksShot", thrownDucksShot);


        // Load the next scene
        if(PlayerPrefs.GetInt("LevelID") < 3)
        {
            SceneManager.LoadScene("LevelFinished");
        } else
        {
            SceneManager.LoadScene("FinalLevel");
        }
    }

    public IEnumerator FadeOutScreen()
    {
        float duration = 2.0f; // Duration in seconds
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

    public void FadingCoroutine()
    {
        StartCoroutine(CloseCurtainsAndFadeOut());
    }

    public float GetRandFloat()
    {
        return Random.Range(0,1f);
    }

    public void ImmediateMenuForButton()
    {
        gameStopped = !gameStopped;
        Time.timeScale = gameStopped ? 0 : 1;
        ImmediateMenu();
    }

    public void ImmediateMenu()
    {
        menu.SetActive(gameStopped);
        dialogManager.orgSpeed = gameStopped ? 0f : 10f;
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
