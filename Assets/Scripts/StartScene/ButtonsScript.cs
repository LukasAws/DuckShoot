using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainPanel;
    public GameObject optionsPanel;

    private AudioSource audioSource;
    private int levelID = 1;

    private void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("LevelID") != 0 || PlayerPrefs.GetInt("LevelID") < 5)
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
        SceneManager.LoadScene($"ArcadeGamePlay {levelID}");
    }

    public void ChangeVolume()
    {
        Slider slider = FindObjectOfType<Slider>();
        audioSource.volume = slider.value;
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
