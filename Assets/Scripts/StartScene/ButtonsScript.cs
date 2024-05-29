using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainPanel;
    public GameObject optionsPanel;

    public int levelID = 1;

    private void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        if (PlayerPrefs.GetInt("LevelID") != 0 && PlayerPrefs.GetInt("LevelID") < 5)
            levelID = PlayerPrefs.GetInt("LevelID");
        else
            levelID = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            levelID = 1;
            PlayerPrefs.SetInt("LevelID", levelID);
        }



        //GetComponent<Slider>().value = PlayerPrefs.GetFloat("GlobalVolume");
    }

    //-------------------------------------------

    public void EnterOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ExitOptions()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        //StartCoroutine(FindObjectOfType<GameManager>().CloseCurtainsAndFadeOut());
        if(levelID<3 && levelID != 0)
            SceneManager.LoadScene($"ArcadeGamePlay {levelID}");
        else
        {
            SceneManager.LoadScene($"ArcadeGamePlay 1");
        }
    }

    public void ChangeVolume()
    {
        Slider slider = FindObjectOfType<Slider>();
        PlayerPrefs.SetFloat("GlobalVolume", slider.value);


        AudioSource[] audioSources = FindObjectsByType<AudioSource>(0);
        foreach (var audio in audioSources)
        {
            audio.volume = PlayerPrefs.GetFloat("GlobalVolume");
        }
    }



    public void Fullscreen()
    {
        Toggle toggle = FindObjectOfType<Toggle>();
        Screen.fullScreen = toggle.isOn;
        //Debug.Log(toggle.isOn);
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
