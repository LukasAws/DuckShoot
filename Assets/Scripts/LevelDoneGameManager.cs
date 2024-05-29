using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LevelDoneGameManager : MonoBehaviour
{
    public Image overlay;
    public TextMeshProUGUI score;
    public TextMeshProUGUI shotsFired;
    public TextMeshProUGUI thrownDucksShot;

    private int _score;
    private int _shots;
    private int _ThrownDucksHit;

    void Awake()
    {
        _score = PlayerPrefs.GetInt("Score");
        _shots = PlayerPrefs.GetInt("ShotsFired");
        _ThrownDucksHit = PlayerPrefs.GetInt("ThrownDucksShot");
    }

    private void Start()
    {

        score.text = "Taskai: " + _score;
        shotsFired.text = "Issauti suviai: " + _shots;
        thrownDucksShot.text = "mestu anciu nusauta: " + _ThrownDucksHit;

        StartCoroutine(OverlayAnim(true));
    }

    public IEnumerator OverlayAnim(bool fadeOut)
    {
        float duration = 2.0f; // Duration in seconds
        float currentTime = 0f;

        // Set the initial color of the overlay
        Color startColor = overlay.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, fadeOut ? 0 : 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(fadeOut ? 1 : 0, fadeOut ? 0 : 1, currentTime / duration);
            overlay.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 0 after finishing the loop
        overlay.color = endColor;

        // Check if the overlay is fully transparent and then disable it
        if (overlay.color.a == (fadeOut ? 0 : 1))
        {
            if (!fadeOut)
            {

                PlayerPrefs.SetInt("LevelID", PlayerPrefs.GetInt("LevelID") + 1);
                if(PlayerPrefs.GetInt("LevelID") < 3)
                    SceneManager.LoadScene($"ArcadeGamePlay {PlayerPrefs.GetInt("LevelID")}");
                else
                    SceneManager.LoadScene($"FinalLevel");
            }
            overlay.gameObject.SetActive(false);
        }
    }

    public void StartAnotherLevel()
    {
        overlay.gameObject.SetActive(true);
        StartCoroutine(OverlayAnim(false));

    }

    public void ExitTheGame()
    {
        Debug.Log("Application Quit");
        SceneManager.LoadScene("TitleScene");
    }
}
