using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor.Rendering;



public class DialogManagerBossLevel : MonoBehaviour
{
    public MovingForDialog dialogMovementScript;
    public TMP_Text dialogText;
    public DialogEntry[] dialogues; // Array of dialog entries
    public int[] scoreThresholds; // Array of score thresholds for each dialog entry
    public GunShootsDog gunShootsDuckScript; // Reference to the GunShootsDuck script
    public bool dialogStarted = false;
    public BossLevelGameManager gameManager;

    private bool dialogFinished = true; // Flag to prevent starting the same dialog again
    internal int currentDialogueIndex = 0;
    private bool isTyping = false;
    private int nextDialogIndex = 0; // Index of the next dialog to be started
    private bool dialogBoxInMotion = false; // Flag to track if the dialog box is moving
    internal RandomAnimationTrigger animationScript;

    static float t = 0;

    private void Awake()
    {
        animationScript = gameManager.GetComponent<RandomAnimationTrigger>();
    }

    void Update()
    {
        if (!dialogStarted && nextDialogIndex < scoreThresholds.Length && !dialogBoxInMotion)
        {
            int score = gunShootsDuckScript.score; // Get the score from the GunShootsDuck script

            if(gameManager.healthScript.health == 0)
            {
                gunShootsDuckScript.score = scoreThresholds[scoreThresholds.Length - 1];
            }

            // Check if a new dialog should be initiated based on the score
            if (score >= scoreThresholds[nextDialogIndex] && dialogFinished)
            {
                StartDialog(nextDialogIndex);
                nextDialogIndex++; // Move to the next dialog index
                animationScript.StopAndPlayDialog();
            }
        }

        // Detect mouse click to handle dialog progression and text skipping
        if (Input.GetMouseButtonDown(0) && dialogStarted && !dialogBoxInMotion)
        {
            if (isTyping)
            {
                // Skip to the end of the current typing animation
                StopAllCoroutines();
                dialogText.text = dialogues[currentDialogueIndex].lines[currentLineIndex]; // Display full text
                isTyping = false;
            }
            else if (currentLineIndex < dialogues[currentDialogueIndex].lines.Length - 1)
            {
                // Move to the next line of dialogue
                currentLineIndex++;
                StartCoroutine(TypeText(dialogues[currentDialogueIndex].lines[currentLineIndex]));
            }
            else
            {
                // All lines of dialog are shown, end the dialog
                dialogMovementScript.InterruptAndEndDialog();
                dialogStarted = false;
                dialogFinished = true;
            }
        }



    }

    private int currentLineIndex = 0; // Index of the current line being displayed

    void StartDialog(int dialogIndex)
    {
        dialogStarted = true;
        dialogFinished = false;
        dialogBoxInMotion = true; // Dialog box is moving
        dialogMovementScript.StartDialog();
        currentDialogueIndex = dialogIndex;
        currentLineIndex = 0; // Reset the line index
        StartCoroutine(WaitForDialogToFinishMoving(dialogIndex));
    }

    IEnumerator WaitForDialogToFinishMoving(int dialogIndex)
    {
        // Wait for the dialog box to finish moving into view
        yield return new WaitForSeconds(1.0f);  // Adjust this time to match your UI movement time
        dialogBoxInMotion = false; // Dialog box has finished moving
        StartCoroutine(TypeText(dialogues[dialogIndex].lines[currentLineIndex]));
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f); // Adjust the typing speed as needed
        }
        isTyping = false;
    }
}


