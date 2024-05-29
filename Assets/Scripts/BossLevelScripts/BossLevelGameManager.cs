using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BossLevelGameManager : MonoBehaviour
{
    internal bool isDialogPlaying = false;
    internal bool isDogShot = false;
    internal int timesDogShot = 0;
    internal DuckMovementBossLevel duckMovement;
    internal RandomAnimationTrigger RAT;

    public int damageStrength = 5;

    public Sprite fallingSprite;

    public HealthBar healthScript;


    public DialogManagerBossLevel dialogManager;

    public TextMeshProUGUI scoreText;
    private int score;

    private GunShootsDog gunScript;


    internal int randomValueForSpawner;
    internal float spawnTime = 0f;

    public Image fadeOverlay;

    private AudioSource audioSource;


    //TODO:
    // Dialog system
    // dog got shot mechanic and add the times shot to invoking the dog move
    // Start is called before the first frame update

    private void Awake()
    {
        gunScript = FindAnyObjectByType<GunShootsDog>();
        score = gunScript.score;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        RAT = FindObjectOfType<RandomAnimationTrigger>();
        AudioSource[] sources = FindObjectsByType<AudioSource>(0);
        foreach(var source in sources)
        {
            source.volume = PlayerPrefs.GetFloat("GlobalVolume");
        }
        audioSource.volume = PlayerPrefs.GetFloat("GlobalVolume");
    }


    // Update is called once per frame
    void Update()
    {
        score = FindAnyObjectByType<GunShootsDog>().score;
        scoreText.text = "Score: " + score;

        
        if(duckMovement.isTouchingGround)
        {
            Debug.Log("FetchDuck");
            StartCoroutine(RAT.FetchDuck());
        }


        randomValueForSpawner = Random.Range(0, 2);

        //Debug.Log(!RAT.isFetching);

        //if (!RAT.isFetching)
        //{
        //    InvokeRepeating("UpdateMoveTrigger", 0f, RAT.updateInterval + timesDogShot);
        //}


        if(healthScript.health == 0)
        {
            PlayerPrefs.SetInt("LevelID", 0);
            //TEMPORARY :
            FadingCoroutine();
        }
        audioSource.volume = PlayerPrefs.GetFloat("GlobalVolume") - fadeOverlay.color.a * PlayerPrefs.GetFloat("GlobalVolume");
    }

    public IEnumerator CloseCurtainsAndFadeOut()
    {

        // Wait for the curtains to fully close (adjust the time to match the animation length)

        yield return new WaitForSeconds(0.0f);

        // Optionally fade the screen to black
        
            StartCoroutine(FadeOutScreen());

            yield return new WaitForSeconds(2.0f);  // Assume animation takes 2 seconds to complete

            // Load the next scene
            SceneManager.LoadScene("LevelFinished 1");

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
}
